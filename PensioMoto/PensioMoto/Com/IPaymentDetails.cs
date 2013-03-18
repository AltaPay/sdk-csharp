using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto.Com
{
	[ComVisible(true)]
	public interface IPaymentDetails
	{
		string ReconciliationIdentifier { get; set; }
		string InvoiceNumber { get; set; }
		double SalesTax { get; set; }

		void AddOrderLine(string Description
			, string ItemId
			, double Quantity
			, double TaxPercent
			, string UnitCode
			, double UnitPrice
			, double Discount
			, string goodsType);
	}
}
