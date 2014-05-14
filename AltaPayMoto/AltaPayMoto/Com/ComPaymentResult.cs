using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using AltaPay.Service;


namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ComPaymentResult : IComPaymentResult
	{
		public string Result { get; set; }
		public string ResultMessage { get; set; }
		public IPayment Payment { get; set; }

		public ComPaymentResult(PaymentResult result)
		{
			Result = result.Result.ToString();
			ResultMessage = result.ResultMerchantMessage;
			if (result.Result == AltaPay.Service.Result.Success)
			{
				Payment = new ComPayment(result.Transaction);

			}
		}
	}
}
