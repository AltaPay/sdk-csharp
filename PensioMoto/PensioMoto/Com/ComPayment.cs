using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using PensioMoto.Service.Dto;

namespace PensioMoto
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ComPayment : IPayment
	{
		public string PaymentId { get; set; }

		public string ShopOrderId { get; set; }

		public string Terminal { get; set; }

		public string PaymentStatus { get; set; }

		public string CreditCardToken { get; set; }

		public string CreditCardMaskedPan { get; set; }

		public double ReservedAmount { get; set; }

		public double CapturedAmount { get; set; }

		public ComPayment(Payment payment)
		{
			CapturedAmount = (double)payment.CapturedAmount;
			CreditCardMaskedPan = payment.CreditCardMaskedPan;
			CreditCardToken = payment.CreditCardToken;
			PaymentId = payment.PaymentId;
			PaymentStatus = payment.PaymentStatus;
			ReservedAmount = (double)payment.ReservedAmount;
			ShopOrderId = payment.ShopOrderId;
			Terminal = payment.Terminal;
		}
	}
}
