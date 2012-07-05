
using System.Xml.Serialization;
namespace PensioMoto.Service.Dto
{
	public class Body
	{
		public string Result { get; set; }
		public string CardHolderErrorMessage { get; set; }
		public string MerchantErrorMessage { get; set; }

		
	}
}
