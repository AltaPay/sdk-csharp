using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using AltaPay.Service.Dto;
using System.Runtime.InteropServices;

namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	public class ComFundingsResult
		: IComFundingsResult
	{
		public ComFundingsResult(FundingsResult result)
		{
			Result = result.Result.ToString();
			ResultMessage = result.ResultMerchantMessage;
			Pages = result.Pages;

			List<IFunding> fundings = new List<IFunding>();
			foreach(Funding funding in result.Fundings)
			{
//TODO				fundings.Add(funding);
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
