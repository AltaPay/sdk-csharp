using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	public interface IComRecurringResult : IComPaymentResult
	{
		IPayment RecurringPayment { get; set; }
	}
}
