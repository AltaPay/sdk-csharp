using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PensioMoto.Service.Dto;

namespace PensioMoto
{
	[ComVisible(true)]
	public class Moto
	{
		MotoDialog _dialog;

		public Moto()
		{
			_dialog = new MotoDialog(new MotoForm(), new MerchantApi());
		}

		public void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal, string orderId, double amount, int currency, string paymentType)
		{
			_dialog.Initialize(gatewayUrl, apiUsername, apiPassword, terminal, orderId, amount, currency, (PaymentType)Enum.Parse(typeof(PaymentType), paymentType));
		}

		public void AddCreditCard(string maskedPan, string cardToken)
		{
			_dialog.AddCreditCard(maskedPan, cardToken);
		}

		public ComPaymentResult Show()
		{
			return new ComPaymentResult(_dialog.Show());
		}
	}

	[ComVisible(true)]
	public class ComPaymentResult
	{
		public string Result { get; set; }
		public string ResultMessage { get; set; }
		public Payment Payment { get; set; }

		public ComPaymentResult(PaymentResult result)
		{
			Result = result.Result.ToString();
			ResultMessage = result.ResultMessage;
			Payment = result.Payment;
		}
	}
}
