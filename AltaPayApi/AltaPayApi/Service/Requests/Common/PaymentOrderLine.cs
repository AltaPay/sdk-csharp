using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaPay.Service
{
	public class PaymentOrderLine
	{
		protected double taxPercent = double.MinValue;
		protected double taxAmount = double.MinValue;
		
		
		public string Description { get; set; }
		public string ItemId { get; set; }
		public double Quantity { get; set; }
		public string UnitCode { get; set; }
		public double UnitPrice { get; set; }
		public double Discount { get; set; }
		public GoodsType GoodsType { get; set; }
		
		
		public double TaxPercent
		{
			get
			{
				return this.taxPercent;
			}
			
			set
			{
				if (this.taxAmount != double.MinValue)
				{
					throw new InvalidOperationException("You are setting TaxPercent, but TaxAmount has already been set on this orderline and you are not allowed to use both");
				}
				
				this.taxPercent = value;
			}
		}
		
		public double TaxAmount
		{
			get
			{
				return this.taxAmount;
			}
			
			set
			{
				if (this.taxPercent != double.MinValue)
				{
					throw new InvalidOperationException("You are setting TaxAmount, but TaxPercent has already been set on this orderline and you are not allowed to use both");
				}
				
				this.taxAmount = value;
			}
		}
	}
}
