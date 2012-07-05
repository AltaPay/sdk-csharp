using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using PensioMoto.Service.Dto;

namespace PensioMoto.Com
{
	public class ComFundingsResult
		: IComFundingsResult
	{
		private int pos = -1;

		public ComFundingsResult(FundingsResult result)
		{
			Result = result.Result.ToString();
			ResultMessage = result.ResultMerchantMessage;
			Pages = result.Pages;

			List<IFunding> fundings = new List<IFunding>();
			foreach(Funding funding in result.Fundings)
			{
				fundings.Add(funding);
			}
			Fundings = fundings.ToArray();
		}

		public string Result { get; set; }
		public string ResultMessage { get; set; }
		public int Pages { get; set; }
		private IFunding[] Fundings { get; set; }

		public int getFundingCount()
		{
			return Fundings.Length;
		}

		public IFunding getFunding(int i)
		{
			return Fundings[i];
		}


	}
}
