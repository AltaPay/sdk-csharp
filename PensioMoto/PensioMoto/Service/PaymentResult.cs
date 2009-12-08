using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
    public class PaymentResult
    {
        public Result Result { get; set; }
		public Payment Payment { get; set; }
    }
}
