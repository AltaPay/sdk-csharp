using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class MultiPaymentRequestRequest
	{
		public MultiPaymentRequestRequest()
		{
			this.Children = new List<MultiPaymentRequestRequestChild>();
		}

		// Required Parameters
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public IList<MultiPaymentRequestRequestChild> Children { get; private set; }

		// Optional parameters
		public string Language { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; }
		public AuthType Type { get; set; }
		public string CreditCardToken { get; set; }
		public string Cookie { get; set; }
		public PaymentRequestConfig Config { get; set; }

		public MultiPaymentRequestRequest AddChild(MultiPaymentRequestRequestChild child)
		{
			this.Children.Add(child);
			return this;
		}
	}
}

