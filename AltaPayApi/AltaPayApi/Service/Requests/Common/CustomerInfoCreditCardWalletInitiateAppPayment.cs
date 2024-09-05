using System;
using System.Collections.Generic;


namespace AltaPay.Service
{
    public class CustomerInfoCreditCardWalletInitiateAppPayment
    {
        public string CardHolderName { get; set; }
        public string ClientAcceptLanguage { get; set; }
        public string ClientUserAgent { get; set; }
        public string ClientIp { get; set; }
        public string ClientAccept { get; set; }
        public string ClientJavaEnabled { get; set; }
        public string ClientColorDepth { get; set; }
        public string ClientScreenHeight { get; set; }
        public string ClientScreenWidth { get; set; }
        public string ClientTimeZone { get; set; }
        public string ClientJavascriptEnabled { get; set; }


        public Dictionary<string, object> AddToDictionary(Dictionary<string, object> parameters)
        {
            parameters.Add("cardholder_name", CardHolderName);
            parameters.Add("client_accept_language", ClientAcceptLanguage);
            parameters.Add("client_user_agent", ClientUserAgent);
            parameters.Add("client_ip", ClientIp);
            parameters.Add("client_accept", ClientAccept);
            parameters.Add("client_java_enabled", ClientJavaEnabled);
            parameters.Add("client_color_depth", ClientColorDepth);
            parameters.Add("client_screen_height", ClientScreenHeight);
            parameters.Add("client_screen_width", ClientScreenWidth);
            parameters.Add("client_time_zone", ClientTimeZone);
            parameters.Add("client_javascript_enabled", ClientJavascriptEnabled);

            return parameters;
        }
    }
}

