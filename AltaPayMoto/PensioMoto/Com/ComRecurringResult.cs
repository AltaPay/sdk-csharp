using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using System.Runtime.InteropServices;

namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ComRecurringResult : ComPaymentResult, IComRecurringResult
	{
		public IPayment RecurringPayment {get;set;}

		public ComRecurringResult(RecurringResult result)
			:base(result)
		{
			RecurringPayment = new ComPayment(result.RecurringPayment);
		}
	}
}
