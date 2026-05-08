using AltaPay.Service;
using System;

namespace Examples
{
    public class CheckoutSessionExamples
    {
        private readonly IMerchantApi _api;

        public CheckoutSessionExamples()
        {
            //This is the URL to connect to your gateway instance. If you are in doubt contact support.
			//For test, use: testgateway.altapaysecure.com
            string gatewayUrl = "https://testgateway.altapaysecure.com/merchant/API/";

            //username to be authenticated on the gateway
            string username = "testuser";

            //provided password for user authentication
            string password = "testpassword";

            //Instatiation of the API helper class which provide all necessary merchant api methods
            //This class requires gateway URL, username and password params are forwarded in the contructor
            _api = new MerchantApi(gatewayUrl, username, password);
        }

        /// <summary>
        /// Example for performing a checkout session request.
        /// </summary>
        public void CreateCheckoutSession()
        {
            CheckoutSessionRequest request = new CheckoutSessionRequest();
            
            request.ShopOrderId = "checkout-req-" + Guid.NewGuid().ToString();
            request.Amount = Amount.Get(120.50, Currency.EUR);
            request.Terminal = "AltaPay Test Terminal";
            request.Terminals.Add("AltaPay Test Terminal");
            request.SessionId = Guid.NewGuid().ToString();

            CheckoutSessionResult result = _api.CheckoutSession(request);

            if (result.Result == Result.Success)
            {
                Console.WriteLine(" Checkout Session created successfully.");
                
                Console.WriteLine(" Session ID: " + result.SessionId);
                Console.WriteLine(" Session Status: " + result.SessionStatus);
            }
            else
            {
                // error messages contain information about what went wrong
                Console.WriteLine(" Failed to create Checkout Session.");
                Console.WriteLine(" Merchant Message: " + result.ResultMerchantMessage);
                Console.WriteLine(" Message: " + result.ResultMessage);
            }
        }
    }
}
