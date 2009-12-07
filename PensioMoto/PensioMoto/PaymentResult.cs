using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto
{
    public class PaymentResult
    {
        public Result Result { get; set; }
        public int PaymentId { get; set; }
        public CardStatus CardStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ShopOrderId { get; set; }
        public string CreditCardToken { get; set; }
        public string CreditCardMaskedPan { get; set; }
        public string Terminal { get; set; }
        public float ReservedAmount { get; set; }
        public float CapturedAmount { get; set; }
    }
}
