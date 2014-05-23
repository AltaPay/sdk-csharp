using System;

namespace AltaPay.Service
{
	public class ReserveParam
	{
		public PaymentSource Source { get; set; }
		public string ShopOrderId {get; set;}
		public Amount Amount { get; set; }
		public AuthType PaymentType { get; set; }

		public string CreditCardToken { get; set;}		// Option1 : Creditcard Token

		public string Pan { get; set;} 					// Option2 : Pan, Month, Year
		public int? ExpiryMonth {get; set; }
		public int? ExpiryYear { get; set; }

		public string Cvc {get; set; }

		public CustomerInfo CustomerInfo { get; set; }

		public ReserveParam ()
		{
			Source = PaymentSource.moto;
			CustomerInfo = new CustomerInfo();
		}
	}
}

