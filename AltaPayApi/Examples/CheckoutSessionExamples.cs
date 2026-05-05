using AltaPay.Service;
using System;

namespace Examples
{
    public class CheckoutSessionExamples
    {
        private readonly IMerchantApi _api;

        public CheckoutSessionExamples()
        {
            // This is the URL to connect to your gateway instance.
            string gatewayUrl = "https://testgateway.altapaysecure.com/merchant/API/";

            // username to be authenticated on the gateway
            string username = "testuser";

            // provided password for user authentication
            string password = "testpassword";

            // Instantiation of the API helper class which provide all necessary merchant api methods
            _api = new MerchantApi(gatewayUrl, username, password);
        }

        /// <summary>
        /// Example for performing a checkout session request.
        /// </summary>
        public void CreateCheckoutSession()
        {
            // Instantiation of the checkout session request class
            CheckoutSessionRequest request = new CheckoutSessionRequest();
            
            // Add Shop Order ID
            request.ShopOrderId = "checkout-req-" + Guid.NewGuid().ToString();

            // Add Amount and Currency
            request.Amount = Amount.Get(120.50, Currency.EUR);

            // Add terminal
            request.Terminal = "AltaPay Test Terminal";

            // Add terminals that are available for the user
            request.Terminals.Add("AltaPay Test Terminal");

            // Optional identifier for a session to be created
            request.SessionId = Guid.NewGuid().ToString();

            // execute checkout session method
            CheckoutSessionResult result = _api.CheckoutSession(request);

            // Result property contains information if the request was successful or not
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
