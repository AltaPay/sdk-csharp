using System;

namespace AltaPay.Service.Dto
{
	public class PaymentRequestBody
		: Body
	{
		public string Url { get; set; }
		public string DynamicJavascriptUrl { get; set; }
	}
}

