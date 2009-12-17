using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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
	}
}
