using System;
using AltaPay.Service;

namespace AltaPay.Service
{
	public class ReserveSubscriptionChargeParam
	{
		public string SubscriptionId { get; set; }
		public Amount Amount { get; set; }
	}
}

