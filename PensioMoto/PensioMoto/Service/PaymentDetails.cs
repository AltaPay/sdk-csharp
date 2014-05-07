﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Moto.Com;
using System.Runtime.InteropServices;


namespace AltaPay.Service
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
			goodsType = goodsType.ToLower();
			if (!goodsType.Equals("shipment") && !goodsType.Equals("handling") && !goodsType.Equals("item"))
			{
				throw new Exception("Invalid goods type, should be one of the following:  shipment, handling, item");
			}
			
			AddOrderLine(Description, ItemId, Quantity, TaxPercent, UnitCode, UnitPrice, Discount, (GoodsType)Enum.Parse(typeof(GoodsType), goodsType, true));
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
	}
}
