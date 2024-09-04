using System;
using System.Collections.Generic;

namespace AltaPay.Service
{
    public class CreditCardWalletInitiateAppPaymentRequest
    {
        public string AppUrl { get; set; }
        public CustomerInfoCreditCardWalletInitiateAppPayment CustomerInfo { get; set; }

        public CreditCardWalletInitiateAppPaymentRequest()
        {
            CustomerInfo = new CustomerInfoCreditCardWalletInitiateAppPayment();
        }
    }
}