using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PensioMoto.Service.Dto
{
	public class Fundings
	{
		[XmlAttribute("numberOfPages")]
		public int Pages;

		[XmlElement]
		public Funding[] Funding { get; set; }
	}
}
