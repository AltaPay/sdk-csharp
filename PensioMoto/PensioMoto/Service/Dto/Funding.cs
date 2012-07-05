using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using PensioMoto.Com;

namespace PensioMoto.Service.Dto
{
	public class Funding
		: IFunding
	{
		[XmlElement("Filename")]
		public string Id { get;set; }
		public string ContractIdentifier { get; set; }
		[XmlArrayItem(ElementName = "Shop")]
		public string[] Shops { get; set; }
		public string Acquirer { get; set; }
		public string FundingDate { get; set; }
		public string Amount { get; set; }
		public string CreatedDate { get; set; }
		public string DownloadLink { get; set; }

		public string ShopString
		{
			get
			{
				return String.Join(", ", Shops);
			}
		}
	}
}
