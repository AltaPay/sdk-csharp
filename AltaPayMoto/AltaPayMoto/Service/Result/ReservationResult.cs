using System;
using AltaPay.Service.Dto;

namespace AltaPay.Service
{
	public class ReservationResult : PaymentResult
	{
		public ReservationResult() 
		{

		}

		public ReservationResult(APIResponse response) : base(response) 
		{

		}
	}
}

