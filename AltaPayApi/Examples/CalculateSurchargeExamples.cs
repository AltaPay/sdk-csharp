using AltaPay.Service;
using AltaPay.Service.Dto;
using System;
using System.Text.RegularExpressions;

namespace Examples
{
    public class CalculateSurchargeExamples
	{
		private readonly IMerchantApi _api;
		public CalculateSurchargeExamples ()
		{
			//This is the URL to connect to your gateway instance. If you are in doubt contact support.
			//For test, use: testgateway.altapaysecure.com
			string gatewayUrl = "https://testgateway.altapaysecure.com/merchant/API/";

			//username to be authenticated on the gateway
			string username = "shop api";

			//provided password for user authentication
			string password = "testpassword";

			//Instatiation of the API helper class which provide all necessary merchant api methods
			//This class requires gateway URL, username and password params are forwarded in the contructor
			_api = new MerchantApi (gatewayUrl, username, password);
		}

		/// <summary>
		/// Example for Calculate Surcharge request.
		/// </summary>
		public void CalculateSurcharge ()
		{
			string paymentId = "52524771";

			CalculateSurchargeRequest calculateSurchargeRequest = new CalculateSurchargeRequest
			{
				PaymentId = paymentId,
				Amount = Amount.Get ("85.80", Currency.DKK),
			};

			CalculateSurchargeResult result = _api.CalculateSurcharge (calculateSurchargeRequest);

			Console.WriteLine ("SurchageAmount: " + result.Surcharge);

		}

	}
}
