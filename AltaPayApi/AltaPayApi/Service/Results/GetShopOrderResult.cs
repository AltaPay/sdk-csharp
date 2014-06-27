using System;
using System.Collections.Generic;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class GetShopOrderResult : ApiResult
	{
		public IEnumerable<Transaction> Payments { get; set; }

		public GetShopOrderResult(APIResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;
				
				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);
	
				Payments = apiResponse.Body.Transactions;
			}
			else
			{
				Result = Result.SystemError;
				ResultMerchantMessage = apiResponse.Header.ErrorMessage;
				ResultMessage = "An error occurred";
			}
		}
	}
}

