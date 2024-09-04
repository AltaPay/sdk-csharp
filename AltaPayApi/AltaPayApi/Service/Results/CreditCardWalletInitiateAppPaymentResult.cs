using System;
using AltaPay.Service.Dto;


namespace AltaPay.Service
{
    public class CreditCardWalletInitiateAppPaymentResult
        : ApiResult
    {
        public RedirectResponse RedirectResponse { get; set; }

        public CreditCardWalletInitiateAppPaymentResult(APIResponse apiResponse)
        {
            if (apiResponse.Header.ErrorCode == 0)
            {
                ResultMessage = apiResponse.Body.CardHolderErrorMessage;
                ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

                if (!String.IsNullOrEmpty(apiResponse.Body.Result))
                    Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

                RedirectResponse = apiResponse.Body.RedirectResponse;
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
