using System;

namespace AltaPay.Service
{
	public class ChargeSubscriptionParam
	{
		public string SubscriptionId { get; set; }
		public Amount Amount { get; set; }
	}
}

