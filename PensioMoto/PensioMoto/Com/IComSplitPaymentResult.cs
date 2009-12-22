using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto.Com
{
	[ComVisible(true)]
	public interface IComSplitPaymentResult : IComPaymentResult
	{
		IPayment SplitPayment1 { get; set; }
		IPayment SplitPayment2 { get; set; }
	}
}
