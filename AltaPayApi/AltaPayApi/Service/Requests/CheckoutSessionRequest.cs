using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class CheckoutSessionRequest
	{
		public string ShopOrderId { get; set; }
		public Amount Amount { get; set; }
		public string Terminal { get; set; }
		public IList<string> Terminals { get; set; }
		public string SessionId { get; set; }

		public CheckoutSessionRequest()
		{
			Terminals = new List<string>();
		}
	}
}
