using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using PensioMoto.Service;

namespace PensioMoto
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ComPaymentResult : IComPaymentResult
	{
		public string Result { get; set; }
		public string ResultMessage { get; set; }
		public IPayment Payment { get; set; }

		public ComPaymentResult(PaymentResult result)
		{
			Result = result.Result.ToString();
			ResultMessage = result.ResultMessage;
			if (result.Result == PensioMoto.Service.Result.Success)
			{
				Payment = new ComPayment
				{
					CapturedAmount = (double)result.Payment.CapturedAmount,
					CreditCardMaskedPan = result.Payment.CreditCardMaskedPan,
					CreditCardToken = result.Payment.CreditCardToken,
					PaymentId = result.Payment.PaymentId,
					PaymentStatus = result.Payment.PaymentStatus,
					ReservedAmount = (double)result.Payment.ReservedAmount,
					ShopOrderId = result.Payment.ShopOrderId,
					Terminal = result.Payment.Terminal
				};
			}
		}
	}
}
