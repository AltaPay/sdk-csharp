using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AltaPay.Service;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AltaPay.Moto
{
	public class MotoDialog : IMotoDialog, IMotoController
	{
		private string _orderId;
		private double _amount;
		private PaymentType _paymentType;
		private int _currency;
		
		private IMotoDialogView _view;
		private IMerchantApi _merchantApi;
		private BlockingQueue _queue;
		private PaymentResult _lastResult;

		public MotoDialog(IMotoDialogView view, IMerchantApi merchantApi)
		{
			_view = view;
			_merchantApi = merchantApi;
			_queue = new BlockingQueue();
		}

		public void Initialize(string gatewayUrl, 
			string apiUsername, 
			string apiPassword, 
			string terminal, 
			string orderId, 
			double amount, 
			int currency, 
			PaymentType paymentType)
		{
			_view.Initialize(this, orderId, amount, paymentType);
			_merchantApi.Initialize(gatewayUrl, apiUsername, apiPassword, terminal);

			_orderId = orderId;
			_amount = amount;
			_currency = currency;
			_paymentType = paymentType;
		}

		public void AddCreditCard(string maskedPan, string cardToken)
		{
			_view.AddCreditCard(maskedPan, cardToken);
		}

		public void SetAvsInfo(string firstName, string lastName, string address, string postalCode, string city, string region, string country, string phone, string email)
		{
			_view.SetAvsInfo(firstName, lastName, address, postalCode, city, region, country, phone, email);
		}

		public PaymentResult Show()
		{
			Thread t = new Thread(new ThreadStart(() => _view.ShowBlocking()));
			t.Start();

			return (PaymentResult)_queue.Dequeue();
		}

		public void PayUsingExistingCreditCard(string cardToken, string cvc, AvsInfo avsInfo)
		{
			HandlePaymentResult(_merchantApi.ReservationOfFixedAmountMOTO(_orderId, _amount, _currency, _paymentType, cardToken, cvc, avsInfo));
		}

		public void PayUsingNewCreditCard(string pan, int expiryMonth, int expiryYear, string cvc, AvsInfo avsInfo)
		{
			HandlePaymentResult(_merchantApi.ReservationOfFixedAmountMOTO(_orderId, _amount, _currency, _paymentType, pan, expiryMonth, expiryYear, cvc, avsInfo));
		}

		public void Cancel()
		{
			_view.Close();
			if (_lastResult != null)
				_queue.Enqueue(_lastResult);
			else
				_queue.Enqueue(new PaymentResult { Result = Result.AbortedByUser });
		}

		private void HandlePaymentResult(PaymentResult result)
		{
			_lastResult = result;
			if (result.Result == Result.Success)
			{
				_view.Close();
				_queue.Enqueue(result);
			}
			else
			{
				_view.EnableView("Payment " + result.Result.ToString().ToLower() + ": " + result.ResultMerchantMessage);
			}
		}
	}
}
