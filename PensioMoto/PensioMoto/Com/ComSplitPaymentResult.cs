﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class ComSplitPaymentResult : ComPaymentResult, IComSplitPaymentResult
	{
		public IPayment SplitPayment1 {get;set;}
		public IPayment SplitPayment2 {get;set;}

		public ComSplitPaymentResult(SplitPaymentResult result)
			: base(result)
		{
			if (result.Result == PensioMoto.Service.Result.Success)
			{
				SplitPayment1 = new ComPayment(result.SplitPayment1);
				SplitPayment2 = new ComPayment(result.SplitPayment2);
			}
		}
	}
}
