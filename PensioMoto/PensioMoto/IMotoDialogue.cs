using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto
{
    public interface IMotoDialogue
    {
        void Initialize(string gatewayUrl, string terminal, string orderId, 
                float amount, int currency, PaymentType paymentType);
        void SetCreditCard(string maskedPan, string cardToken);
        PaymentResult Show();
    }
}
