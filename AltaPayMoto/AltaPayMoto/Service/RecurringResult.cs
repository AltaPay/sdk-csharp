using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class RecurringResult : PaymentResult
	{
		public Transaction RecurringPayment { get; set; }

		public RecurringResult()
			:base()
		{
		}

		public RecurringResult(APIResponse apiResponse)
			:base(apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0 && apiResponse.Body.Transactions != null && apiResponse.Body.Transactions.Length > 1)
			{
				RecurringPayment = apiResponse.Body.Transactions[1];
			}
		}
	}
}
