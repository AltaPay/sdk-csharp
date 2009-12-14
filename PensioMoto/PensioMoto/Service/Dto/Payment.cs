using System;
using System.Xml.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PensioMoto.Service.Dto
{
	[ComVisible(true)]
	[GuidAttribute("d5f7be20-5d6f-4690-8b2e-ac60d1f37c5a")]
	public class Payment
	{
		[XmlElement(ElementName = "TransactionId")]
		[DispIdAttribute(1)]
		public int PaymentId { get; set; }
		[DispIdAttribute(2)]
		public string ShopOrderId { get; set; }
		[DispIdAttribute(3)]
		public string Terminal { get; set; }

		[XmlElement(ElementName = "TransactionStatus")]
		[DispIdAttribute(4)]
		public string PaymentStatus { get; set; }
		[DispIdAttribute(5)]
		public string CreditCardToken { get; set; }
		[DispIdAttribute(6)]
		public string CreditCardMaskedPan { get; set; }

		[XmlIgnore]
		[DispIdAttribute(7)]
		public Decimal ReservedAmount 
		{
			get
			{
				Decimal result = new Decimal();

				if (Decimal.TryParse(ReservedAmountString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out result))
					return result;
				else
					return new Decimal(0);
			}
		}
		[XmlElement(ElementName = "ReservedAmount")]
		[DispIdAttribute(8)]
		public string ReservedAmountString;

		[XmlIgnore]
		[DispIdAttribute(9)]
		public Decimal CapturedAmount
		{
			get
			{
				Decimal result = new Decimal();
				if (Decimal.TryParse(CapturedAmountString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out result))
					return result;
				else
					return new Decimal(0);
			}
		}
		[XmlElement(ElementName = "CapturedAmount")]
		[DispIdAttribute(10)]
		public string CapturedAmountString;
	}
}
