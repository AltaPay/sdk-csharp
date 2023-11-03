using System;
using System.Collections.Generic;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
    public class GetTerminalsResult : ApiResult
    {
        public IEnumerable<Terminal> Terminals { get; }

        public GetTerminalsResult(APIResponse apiResponse)
        {
            if (apiResponse.Header.ErrorCode == 0)
            {
                ResultMessage = apiResponse.Body.CardHolderErrorMessage;
                ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

                if (!String.IsNullOrEmpty(apiResponse.Body.Result))
                    Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);
                Terminals = apiResponse.Body.Terminals;
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