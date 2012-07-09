using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service.Dto;
using System.Net;

namespace PensioMoto.Service
{
	public class FundingsResult
		: ApiResult
	{
		public List<Funding> Fundings { get; set; }
		public int Pages { get; set; }

		public FundingsResult()
		{
			Fundings = new List<Funding>();
		}

		public FundingsResult(FundingsApiResponse apiResponse, NetworkCredential networkCredential)
		{
			Fundings = new List<Funding>();
			if (apiResponse.Header.ErrorCode == 0)
			{
				ResultMessage = apiResponse.Body.CardHolderErrorMessage;
				ResultMerchantMessage = apiResponse.Body.MerchantErrorMessage;

				if (!String.IsNullOrEmpty(apiResponse.Body.Result))
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result);

				Fundings = new List<Funding>(apiResponse.Body.Fundings.Funding);
				foreach (Funding funding in Fundings)
				{
					funding.inject(networkCredential);
				}
				Pages = apiResponse.Body.Fundings.Pages;
			}
			else
			{
				Result = Result.SystemError;
				ResultMessage = apiResponse.Header.ErrorMessage;
			}
		}
	}
}
