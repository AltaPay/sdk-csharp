﻿using System.Runtime.InteropServices;
using PensioMoto.Service.Dto;
using System;

namespace PensioMoto.Service
{
    public class PaymentResult
		: ApiResult
    {
		public Payment Payment { get; set; }

		public PaymentResult()
		{
		}

		public PaymentResult(PaymentApiResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

				Payment = (apiResponse.Body.Transactions != null && apiResponse.Body.Transactions.Length > 0 ? apiResponse.Body.Transactions[0] : null);
			}
			else
			{
				Result = Result.SystemError;
				ResultMessage = apiResponse.Header.ErrorMessage;
			}
		}
    }
}
