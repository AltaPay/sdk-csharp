using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PensioMoto.Service.Dto
{
	[XmlRoot(ElementName = "APIResponse")]
	public class FundingsApiResponse
		: ApiResponse
	{
		[XmlElement]
		public FundingsBody Body { get; set; }
	}
}
