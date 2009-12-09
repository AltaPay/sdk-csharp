using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto.Service
{
	[GuidAttribute("81534a3f-1233-4fb3-b83e-88d44fe9607d")]
	[ComVisible(true)]
	public class Payment
	{
		public int PaymentId { get; set; }
		public string PaymentStatus { get; set; }
		public string ShopOrderId { get; set; }
		public string CreditCardToken { get; set; }
		public string CreditCardMaskedPan { get; set; }
		public string Terminal { get; set; }
		public double ReservedAmount { get; set; }
		public double CapturedAmount { get; set; }
	}
}
