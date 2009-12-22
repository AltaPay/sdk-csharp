using System.Runtime.InteropServices;
using PensioMoto.Service.Dto;
using System;

namespace PensioMoto.Service
{
    public class PaymentResult
    {
        public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public Payment Payment { get; set; }

		public PaymentResult()
		{
		}

		public PaymentResult(ApiResponse apiResponse)
		{
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				Payment = (apiResponse.Body.Transactions != null && apiResponse.Body.Transactions.Length > 0 ? apiResponse.Body.Transactions[0] : null);

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);
			}
			else
			{
				Result = Result.SystemError;
				ResultMessage = apiResponse.Header.ErrorMessage;
			}
		}
    }
}
