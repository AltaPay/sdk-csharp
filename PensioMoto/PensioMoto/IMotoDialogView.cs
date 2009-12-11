﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Windows.Forms;

namespace PensioMoto
{
	public interface IMotoDialogView
	{
		void Initialize(IMotoController controller, string orderId, double amount, PaymentType paymentType);
		void AddCreditCard(string maskedPan, string cardToken);
		void ShowBlocking();
		void Close();
		void EnableView(string status);
	}
}