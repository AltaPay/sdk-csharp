using System;
using System.Collections.Generic;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
    public class CheckoutSessionResult : ApiResult
    {
        public string SessionId { get; set; }
        public string SessionStatus { get; set; }
        public IList<string> SupportedTerminals { get; set; }

        public CheckoutSessionResult(APIResponse apiResponse)
        {
            SupportedTerminals = new List<string>();

            if (apiResponse.Header.ErrorCode == 0)
            {
                Result = Result.Success;

                if (apiResponse.Body.Session != null)
                {
                    SessionId = apiResponse.Body.Session.Id;
                    SessionStatus = apiResponse.Body.Session.Status;

                    if (apiResponse.Body.Session.SupportedTerminals != null)
                    {
                        SupportedTerminals = new List<string>(apiResponse.Body.Session.SupportedTerminals);
                    }
                }
            }
            else
            {
                Result = Result.Error;
                ResultMerchantMessage = apiResponse.Header.ErrorMessage;
                ResultMessage = "An error occurred";
            }
        }
    }
}
