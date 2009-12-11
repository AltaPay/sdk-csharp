using System.Runtime.InteropServices;
using PensioMoto.Service.Dto;

namespace PensioMoto.Service
{
	[ComVisible(true)]
    public class PaymentResult
    {
        public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public Payment Payment { get; set; }
    }
}
