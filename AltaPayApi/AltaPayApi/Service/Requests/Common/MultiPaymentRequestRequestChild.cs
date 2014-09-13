using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class MultiPaymentRequestRequestChild
	{
		// Required
		public Amount Amount { get; set; }

		// Optional
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; }
		public AuthType Type { get; set; }
		public ShippingType ShippingType { get; set; }
		public string SalesReconciliationIdentifier { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set;}
	}
}
