using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay;
using System.Runtime.InteropServices;


namespace AltaPay.Service
{
    [ClassInterface(ClassInterfaceType.None)]
	public class PaymentRequestParam
	{
		// Required Parameters
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public Amount Amount { get; set; }
		
		// Optional parameters
		public string Language { get; set; }
		public IDictionary<string,object> PaymentInfos { get; set; }
		public AuthType Type { get; set; }
		public string CreditCardToken { get; set; }
		public string SalesReconciliationIdentifier { get; set; }
		public string SalesInvoiceNumber { get; set; }
		public double SalesTax { get; set; }
		public string Cookie { get; set; }
		public PaymentRequestConfig Config { get; set; }
		public CustomerInfo CustomerInfo { get; set; }
		public string CustomerCreatedDate { get; set; }
		public IList<PaymentOrderLine> OrderLines { get; set;}
		public ShippingType ShippingType { get; set; }
		public string OrganisationNumber { get; set; } // If the organisation_number parameter is given the organisation number field in the invoice payment form is prepopulated, and if no other payment options is enabled on the terminal the form will auto submit.
		public AccountOffer AccountOffer { get; set; } // To require having account enabled for an invoice payment for this specific customer, set this to required. To disable account for this specific customer, set to disabled.
		
		
		public PaymentRequestParam() {
			Config = new PaymentRequestConfig();	
			CustomerInfo = new CustomerInfo();
			OrderLines = new List<PaymentOrderLine>();
			PaymentInfos = new Dictionary<string, object>();
		}
	}
}
