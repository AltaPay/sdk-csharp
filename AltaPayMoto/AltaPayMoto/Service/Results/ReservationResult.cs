using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class ReserveResult : PaymentResult
	{
		public ReserveResult() : base()
		{

		}

		public ReserveResult(APIResponse response) : base(response) 
		{

		}
	}
}

