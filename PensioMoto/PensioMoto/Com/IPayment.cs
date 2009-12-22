﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	public interface IPayment
	{
		string PaymentId { get; set; }
		string ShopOrderId { get; set; }
		string Terminal { get; set; }
		string PaymentStatus { get; set; }
		string CreditCardToken { get; set; }
		string CreditCardMaskedPan { get; set; }
		double ReservedAmount { get; set; }
		double CapturedAmount { get; set; }
	}
}