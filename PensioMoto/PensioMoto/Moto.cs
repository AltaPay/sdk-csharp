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
	[ClassInterface(ClassInterfaceType.None)]
	[GuidAttribute("81dac6af-1e5a-4327-a9ef-18a6f180627c")]
	public class Moto
	{
		MotoDialog _dialog;

		public Moto()
		{
			_dialog = new MotoDialog(new MotoForm(), new MerchantApi());
		}

		[DispIdAttribute(1)]
		public void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal, string orderId, double amount, int currency, string paymentType)
		{
			_dialog.Initialize(gatewayUrl, apiUsername, apiPassword, terminal, orderId, amount, currency, (PaymentType)Enum.Parse(typeof(PaymentType), paymentType));
		}

		[DispIdAttribute(2)]
		public void AddCreditCard(string maskedPan, string cardToken)
		{
			_dialog.AddCreditCard(maskedPan, cardToken);
		}

		[DispIdAttribute(3)]
		public ComPaymentResult Show()
		{
			return new ComPaymentResult(_dialog.Show());
		}
	}

	[ComVisible(true)]
	[GuidAttribute("5f0ff6df-4bab-47d9-b53c-272cdd64bf69")]
	public class ComPaymentResult
	{
		[DispIdAttribute(1)]
		public string Result { get; set; }
		[DispIdAttribute(2)]
		public string ResultMessage { get; set; }
		[DispIdAttribute(3)]
		public Payment Payment { get; set; }

		public ComPaymentResult(PaymentResult result)
		{
			Result = result.Result.ToString();
			ResultMessage = result.ResultMessage;
			Payment = result.Payment;
		}
	}
}
