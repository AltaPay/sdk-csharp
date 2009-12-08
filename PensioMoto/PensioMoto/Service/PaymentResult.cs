using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PensioMoto.Service
{
    public class PaymentResult
    {
        public Result Result { get; set; }
		public string ResultMessage { get; set; }
		public Payment Payment { get; set; }
    }
}
