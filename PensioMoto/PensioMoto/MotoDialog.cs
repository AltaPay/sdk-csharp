using System;
using System.Collections.Generic;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	public class MotoDialog : IMotoDialog
	{
		/*
		private string _orderId;
		private double _amount;
		private PaymentType _paymentType;
		private int _currency;
		*/
		private IMotoDialogView _view;
		private IMerchantApi _merchantApi;

		public MotoDialog(IMotoDialogView view, IMerchantApi merchantApi)
		{
			_view = view;
			_merchantApi = merchantApi;
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
			_view.Initialize(orderId, amount, paymentType);
			_merchantApi.Initialize(gatewayUrl, terminal, apiUsername, apiPassword);

			/*
			_orderId = orderId;
			_amount = amount;
			_currency = currency;
			_paymentType = paymentType;
			 */
		}

		public void SetCreditCard(string maskedPan, string cardToken)
		{
			throw new NotImplementedException();
		}

		public PaymentResult Show()
		{
			throw new NotImplementedException();
		}
	}
}
