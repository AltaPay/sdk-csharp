using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	public interface IComSplitPaymentResult
	{
		string Result { get; set; }
		string ResultMessage { get; set; }
		IPayment Payment { get; set; }

		IPayment SplitPayment1 { get; set; }
		IPayment SplitPayment2 { get; set; }
	}
}
