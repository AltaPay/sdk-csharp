
namespace PensioMoto.Service.Dto
{
	public class Body
	{
		public string Result { get; set; }
		public string CardHolderErrorMessage { get; set; }
		public Transaction[] Transactions { get; set; }
	}
}
