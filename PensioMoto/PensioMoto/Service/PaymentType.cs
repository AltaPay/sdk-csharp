﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
    public enum PaymentType
    {
        payment, paymentAndCapture, subscription, subscriptionAndCharge, verifyCard
    }
}
