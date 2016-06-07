using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
	public class InvoiceReservationRequest
	{

		// required
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public Amount Amount { get; set; }
		//public Currency Currency { get; set; }
		public CustomerInfo CustomerInfo { get; set; }

		// Optional parameters
		public AuthType? AuthType { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; } // transaction infos
		public string AccountNumber { get; set; }
		public string BankCode { get; set; }
		public FraudService? FraudService { get; set; }
		public PaymentSource? PaymentSource { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set; }
		public string OrganisationNumber { get; set; } 
		public string PersonalIdentifyNumber { get; set; } 
		public string BirthDate { get; set; } // YYYY-MM-DD

		public InvoiceReservationRequest()
		{
			AuthType = null;
			FraudService = null;
			PaymentSource = null;
			CustomerInfo = new CustomerInfo();
			OrderLines = new List<PaymentOrderLine>();
		}
	}

}
