﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;
using System.Threading;

namespace PensioMoto
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
			_merchantApi.Initialize(gatewayUrl, terminal, apiUsername, apiPassword);

			_orderId = orderId;
			_amount = amount;
			_currency = currency;
			_paymentType = paymentType;
		}

		public void SetCreditCard(string maskedPan, string cardToken)
		{
			_view.SetCreditCard(maskedPan, cardToken);
		}

		public PaymentResult Show()
		{
			Thread t = new Thread(new ThreadStart(() => _view.ShowBlocking()));
			t.Start();

			return (PaymentResult)_queue.Dequeue();
		}

		public void PayUsingExistingCreditCard(string cardToken, int cvc)
		{
			HandlePaymentResult(_merchantApi.ReservationOfFixedAmountMOTO(_orderId, _amount, _currency, _paymentType, cardToken, cvc));
		}

		public void PayUsingNewCreditCard(string pan, int expiryMonth, int expiryYear, int cvc)
		{
			HandlePaymentResult(_merchantApi.ReservationOfFixedAmountMOTO(_orderId, _amount, _currency, _paymentType, pan, expiryMonth, expiryYear, cvc));
		}

		public void Cancel()
		{
			_view.Close();
			_queue.Enqueue(null);
		}

		private void HandlePaymentResult(PaymentResult result)
		{
			if (result.Result == Result.Success)
			{
				_view.Close();
				_queue.Enqueue(result);
			}
			else
				_view.EnableView("Payment "+result.Result.ToString().ToLower());
		}
	}
}
