using System;
using System.Xml.Serialization;
using System.Globalization;

namespace PensioMoto.Service.Dto
{
	public class Payment
	{
		[XmlElement(ElementName = "TransactionId")]
		public string PaymentId { get; set; }
		public string ShopOrderId { get; set; }
		public string Terminal { get; set; }

		[XmlElement(ElementName = "TransactionStatus")]
		public string PaymentStatus { get; set; }
		public string CardStatus { get; set; }
		public string CreditCardToken { get; set; }
		public string CreditCardMaskedPan { get; set; }

		[XmlIgnore]
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
		public string ReservedAmountString {get;set;}

		[XmlIgnore]
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
		public string CapturedAmountString {get;set;}

		[XmlIgnore]
		public Decimal RefundedAmount
		{
			get
			{
				Decimal result = new Decimal();
				if (Decimal.TryParse(RefundedAmountString, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out result))
					return result;
				else
					return new Decimal(0);
			}
		}
		[XmlElement(ElementName = "RefundedAmount")]
		public string RefundedAmountString { get; set; }

		public string AddressVerification { get; set; }
		public string AddressVerificationDescription { get; set; }
	}

}
