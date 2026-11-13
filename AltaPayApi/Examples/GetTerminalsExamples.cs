using AltaPay.Service;
using AltaPay.Service.Tests.Integration;
using AltaPay.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    public class GetTerminalsExamples
    {
        private readonly IMerchantApi _api;
        public GetTerminalsExamples()
        {
            //This is the URL to connect to your gateway instance. If you are in doubt contact support.
            //For test, use: testgateway.altapaysecure.com
            string gatewayUrl = GatewayConstants.gatewayUrl;

            //username to be authenticated on the gateway
            string username = GatewayConstants.username;

            //provided password for user authentication
            string password = GatewayConstants.password;

            //Instatiation of the API helper class which provide all necessary merchant api methods
            //This class requires gateway URL, username and password params are forwarded in the contructor
            _api = new MerchantApi(gatewayUrl, username, password);
        }

        /// <summary>
        /// Example for performing simple refund request. 
        /// Payment has to be in captured state in order to refund be successful.
        /// </summary>
        public void GetTerminals()
        {
            GetTerminalsResult result = _api.GetTerminals();

             foreach (Terminal terminal in result.Terminals)
            {
                Console.WriteLine("Title: " + terminal.Title + "\n");
                Console.WriteLine("Country: " + terminal.Country + "\n");
                Console.WriteLine("Merchant ID: " + terminal.MerchantId + "\n");
                Console.WriteLine("Primary Method Nature: " + terminal.PrimaryMethod.Nature + "\n");
                Console.WriteLine("Primary Method Identifier: " + terminal.PrimaryMethod.Identifier + "\n");
                Console.WriteLine("Natures: \n");

                foreach (var nature in terminal.Natures)
                {
                    Console.WriteLine(nature + "\n");
                }

                Console.WriteLine("Methods: \n");

                foreach (var method in terminal.Methods)
                {
                    Console.WriteLine(method + "\n");
                }

                Console.WriteLine("Currencies: \n");

                foreach (var currency in terminal.Currencies)
                {
                    Console.WriteLine(currency + "\n");
                }

                Console.WriteLine("Method configurations: \n");

                if (terminal.MethodConfigurations != null)
                {
                    foreach (var config in terminal.MethodConfigurations)
                    {
                        Console.WriteLine(config.method + " (identifier: " + config.identifier + ")\n");

                        // configuration values specific to the method, e.g. wallet environment or supported customer types
                        if (config.Any != null)
                        {
                            foreach (var element in config.Any)
                            {
                                Console.WriteLine("    " + element.Name + ": " + element.InnerText + "\n");
                            }
                        }
                    }
                }

                if (terminal.Schemes != null && terminal.Schemes.Scheme != null)
                {
                    Console.WriteLine("Schemes: \n");

                    foreach (var scheme in terminal.Schemes.Scheme)
                    {
                        Console.WriteLine(scheme.code + ": " + scheme.Value + "\n");
                    }
                }

                Console.WriteLine("CanUseCredit: " + terminal.CanUseCredit.ToString() + "\n");
                Console.WriteLine("CanIssueNewCredit: " + terminal.CanIssueNewCredit.ToString() + "\n");

            }

        }
        
    }
}
