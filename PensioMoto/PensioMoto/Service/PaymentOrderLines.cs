using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
	public class PaymentOrderLines
	{
		public List<PaymentOrderLine> Lines { get; set; }
		
		public void Clear()
		{
			Lines.Clear();
		}
		
		public void AddOrderLine(String Description
			, String ItemId
			, double Quantity
			, double TaxPercent
			, String UnitCode
			, double UnitPrice
			, double Discount
			, String GoodsType)
		{
			PaymentOrderLine orderLine = new PaymentOrderLine();
			orderLine.Description = Description;
			orderLine.ItemId = ItemId;
			orderLine.Quantity = Quantity;
			orderLine.TaxPercent = TaxPercent;
			orderLine.UnitCode = UnitCode;
			orderLine.UnitPrice = UnitPrice;
			orderLine.Discount = Discount;
			orderLine.GoodsType = GoodsType;
			
			Lines.Add(orderLine);
		}
	}
}
