using System;

namespace AltaPay.Service.Tests.Integration
{
	public class GatewayConstants
	{
		//This is the URL to connect to your gateway instance. If you are in doubt contact support.
		//For test, use: testgateway.altapaysecure.com
		public const string gatewayUrl = "https://testgateway.altapaysecure.com/merchant/API/";
		public const string username = "shop api";
		public const string password = "testpassword";
		public const string terminal = "AltaPay Test Terminal";
		public const string klarnaTerminal = "AltaPay Klarna DKK Test Terminal";

		public const string PreauthTransactionStatus = "preauth";
		public const string CreditedTransactionStatus = "credited";
		
	}
}

