using System;

namespace AltaPay.Service
{
	public class RefundRequest
	{
		public string PaymentId { get; set; }
		public Amount Amount { get; set; }
		public string ReconciliationId { get; set; }
	}
}

