using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class CalculateSurchargeResult : ApiResult
	{
		public decimal Surcharge { get; set; }

		public CalculateSurchargeResult (APIResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;
				Surcharge = Convert.ToDecimal (apiResponse.Body.SurchargeAmount);
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

