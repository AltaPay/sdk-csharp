using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaPay.Service
{
    public enum AuthType
    {
        payment, paymentAndCapture, subscription, subscriptionAndCharge, verifyCard
    }
}
