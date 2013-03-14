using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
	/**
	* description	Description of item.	String (255)
	* itemId	Item number.	String (100)
	* quantity	Quantity.	Decimal
	* taxPercent	Decimal	Decimal
	* unitCode	Measurement unit, e.g., kg.	String (50)
	* unitPrice	Unit price excluding sales tax	Decimal
	* discount	The discount in percent	Decimal
	* goodsType	The type of order line it is. Should be one of the following	shipment|handling|item
	*/	
	public class PaymentOrderLine
	{
		public String Description { get; set; }
		public String ItemId { get; set; }
		public double Quantity { get; set; }
		public double TaxPercent { get; set; }
		public String UnitCode { get; set; }
		public double UnitPrice { get; set; }
		public double Discount { get; set; }
		// Must be one of: shipment|handling|item
		public String GoodsType { get; set; }
	}
}
