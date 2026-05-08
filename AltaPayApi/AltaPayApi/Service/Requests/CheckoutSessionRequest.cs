using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class CheckoutSessionRequest : PaymentRequestRequest
	{
		public IList<string> Terminals { get; set; }
		public string SessionId { get; set; }

		public CheckoutSessionRequest()
		{
			Terminals = new List<string>();
		}
	}
}
