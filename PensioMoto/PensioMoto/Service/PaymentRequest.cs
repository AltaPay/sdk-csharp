using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto;
using System.Runtime.InteropServices;


namespace PensioMoto.Service
{
    [ClassInterface(ClassInterfaceType.None)]
	public class PaymentRequest
	{
		// Required Parameters
		public string Terminal { get; set; }
		public string ShopOrderId { get; set; }
		public double Amount { get; set; }
		public Currency Currency { get; set; }
		
		// Optional parameters
		public string Language { get; set; }
		private Dictionary<string,Object> infos = new Dictionary<string,Object>();
		public PaymentType Type { get; set; }
		public string CreditCardToken { get; set; }
		public string SalesReconciliationIdentifier { get; set; }
		public string SalesInvoiceNumber { get; set; }
		public double SalesTax { get; set; }
		public string Cookie { get; set; }
		private PaymentRequestConfig config = new PaymentRequestConfig();
		private CustomerInfo CustomerInfo = new CustomerInfo();
		public string CustomerCreatedDate { get; set; }		
		private List<PaymentOrderLine> lines = new List<PaymentOrderLine>();
		public ShippingType ShippingType { get; set; }
		public string OrganisationNumber { get; set; } // If the organisation_number parameter is given the organisation number field in the invoice payment form is prepopulated, and if no other payment options is enabled on the terminal the form will auto submit.
		public AccountOffer AccountOffer { get; set; } // To require having account enabled for an invoice payment for this specific customer, set this to required. To disable account for this specific customer, set to disabled.
		
		public PaymentRequestConfig Config
		{
			get { return config; }
		}
		
		public List<PaymentOrderLine> GetLines()
		{
			return lines;
		}
		
		public Dictionary<string,Object> GetInfos()
		{
			return infos;
		}
		
		public CustomerInfo GetCustomerInfo()
		{
			return CustomerInfo;
		}
		
		public void AddOrderLine(string Description
			, string ItemId
			, double Quantity
			, double TaxPercent
			, string UnitCode
			, double UnitPrice
			, double Discount
			, GoodsType goodsType)
		{
			PaymentOrderLine orderLine = new PaymentOrderLine();
			orderLine.Description = Description;
			orderLine.ItemId = ItemId;
			orderLine.Quantity = Quantity;
			orderLine.TaxPercent = TaxPercent;
			orderLine.UnitCode = UnitCode;
			orderLine.UnitPrice = UnitPrice;
			orderLine.Discount = Discount;
			orderLine.GoodsType = goodsType;
			
			lines.Add(orderLine);
		}
		
		public void AddInfo(string key, string value)
		{
			infos.Add(key, value);
		}
	}
}
