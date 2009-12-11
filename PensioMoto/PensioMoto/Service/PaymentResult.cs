using System.Runtime.InteropServices;

namespace PensioMoto.Service
{
	[GuidAttribute("983e2773-6f20-4635-8114-f86a4f092f53")]
	[ComVisible(true)]
    public class PaymentResult
    {
        public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public Payment Payment { get; set; }
    }
}
