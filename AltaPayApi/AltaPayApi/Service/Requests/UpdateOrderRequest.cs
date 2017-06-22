using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class UpdateOrderRequest
	{
		public string PaymentId { get; }
		public IList<PaymentOrderLine> OrderLines { get; } 

		public UpdateOrderRequest(string paymentId, IList<PaymentOrderLine> orderLines)
		{
		
			if (orderLines.Count != 2)
			{
				throw new ArgumentException("orderLines must contain exactly two elements");
			}

			PaymentId = paymentId;
			OrderLines = orderLines;
		
		}
	}
}

