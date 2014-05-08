using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Globalization;

namespace AltaPay.Service.Dto
{
	public class ReconciliationIdentifier
	{
		public string Id { get; set; }
		[XmlElement(ElementName = "Amount")]
		public string AmountString { get; set; }
		[XmlIgnore]
		public Decimal Amount
		{
			get
			{
				Decimal result = new Decimal();
				if (Decimal.TryParse(AmountString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out result))
					return result;
				else
					return new Decimal(0);
			}
		}
		public string Type { get; set; }
		[XmlElement(ElementName = "Date")]
		public string DateString { get; set; }
		[XmlIgnore]
		public DateTime Date 
		{ 
			get 
			{
				return DateTime.Parse(DateString);
			} 
		}
	}
}
