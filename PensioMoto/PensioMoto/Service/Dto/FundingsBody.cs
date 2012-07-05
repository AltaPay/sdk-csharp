using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PensioMoto.Service.Dto
{
	public class FundingsBody
		: Body
	{
		[XmlElement]
		public Fundings Fundings;
	}
}
