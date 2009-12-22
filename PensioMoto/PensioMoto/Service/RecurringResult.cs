using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service.Dto;

namespace PensioMoto.Service
{
	public class RecurringResult : PaymentResult
	{
		public Payment RecurringPayment { get; set; }

		public RecurringResult()
			:base()
		{
		}

		public RecurringResult(ApiResponse apiResponse)
			:base(apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0 && apiResponse.Body.Transactions != null && apiResponse.Body.Transactions.Length > 1)
			{
				RecurringPayment = apiResponse.Body.Transactions[1];
			}
		}
	}
}
