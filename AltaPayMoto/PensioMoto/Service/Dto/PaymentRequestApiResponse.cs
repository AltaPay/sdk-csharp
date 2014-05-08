using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AltaPay.Service.Dto
{
	[XmlRoot(ElementName="APIResponse")]
	public class PaymentRequestApiResponse
			: ApiResponse
	{
		public PaymentRequestBody Body { get; set; }
	}
}
