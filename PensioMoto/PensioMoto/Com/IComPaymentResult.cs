using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	public interface IComPaymentResult
	{
		string Result { get; set; }
		string ResultMessage { get; set; }
		IPayment Payment { get; set; }
	}
}
