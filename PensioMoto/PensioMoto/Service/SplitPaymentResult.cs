using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service.Dto;

namespace PensioMoto.Service
{
	public class SplitPaymentResult : PaymentResult
	{
		public Payment SplitPayment1 { get; set; }
		public Payment SplitPayment2 { get; set; }

		public SplitPaymentResult()
			:base()
		{
			
		}

		public SplitPaymentResult(ApiResponse apiResponse)
			: base(apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				if (apiResponse.Body.Transactions != null)
				{
					SplitPayment1 = apiResponse.Body.Transactions[1];
					SplitPayment2 = apiResponse.Body.Transactions[2];
				}
			}
		}
	}
}
