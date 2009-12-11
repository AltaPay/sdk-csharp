using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;

namespace PensioMoto
{
	public class Moto : IMotoDialog
	{
		MotoDialog _dialog;

		public Moto()
		{
			_dialog = new MotoDialog(new MotoForm(), new MerchantApi());
		}

		public void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal, string orderId, double amount, int currency, PensioMoto.Service.PaymentType paymentType)
		{
			_dialog.Initialize(gatewayUrl, apiUsername, apiPassword, terminal, orderId, amount, currency, paymentType);
		}

		public void AddCreditCard(string maskedPan, string cardToken)
		{
			_dialog.AddCreditCard(maskedPan, cardToken);
		}

		public PensioMoto.Service.PaymentResult Show()
		{
			return _dialog.Show();
		}
	}
}
