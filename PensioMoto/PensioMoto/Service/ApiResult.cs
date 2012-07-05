using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service.Dto;

namespace PensioMoto.Service
{
	public class ApiResult
	{
		public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public string ResultMerchantMessage { get; set; }
		public Payment Payment { get; set; }
	}
}
