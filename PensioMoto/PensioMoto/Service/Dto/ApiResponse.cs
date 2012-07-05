using System.Xml.Serialization;

namespace PensioMoto.Service.Dto
{
	[XmlRoot(ElementName="APIResponse")]
	public class ApiResponse
	{
		public Header Header { get; set; }
	}
}
