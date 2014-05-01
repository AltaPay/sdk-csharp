using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto;
using System.Runtime.InteropServices;


namespace PensioMoto.Service
{
    [ClassInterface(ClassInterfaceType.None)]
	public class PaymentDetails : IPaymentDetails
	{
		private List<PaymentOrderLine> lines = new List<PaymentOrderLine>();

		public List<PaymentOrderLine> getLines()
		{
			return lines;
		}

		public string ReconciliationIdentifier { get; set; }
		public string InvoiceNumber { get; set; }
		public double SalesTax { get; set; }

		public void AddOrderLine(string Description
			, string ItemId
			, double Quantity
			, double TaxPercent
			, string UnitCode
			, double UnitPrice
			, double Discount
			, string goodsType)
		{
			PaymentOrderLine orderLine = new PaymentOrderLine();
			orderLine.Description = Description;
			orderLine.ItemId = ItemId;
			orderLine.Quantity = Quantity;
			orderLine.TaxPercent = TaxPercent;
			orderLine.UnitCode = UnitCode;
			orderLine.UnitPrice = UnitPrice;
			orderLine.Discount = Discount;
			if (goodsType != "shipment" && goodsType != "handling" && goodsType != "item")
			{
				throw new Exception("Invalid goods type, should be one of the following:  shipment, handling, item");
			}
			orderLine.GoodsType = (GoodsType)Enum.Parse(typeof(GoodsType), goodsType);
			
			lines.Add(orderLine);
		}
	}
}
