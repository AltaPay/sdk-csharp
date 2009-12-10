using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;

namespace PensioMoto
{
	public interface IMotoDialogView
	{
		void Initialize(string orderId, double amount, PaymentType paymentType);
		void SetCreditCard(string maskedPan, string cardToken);
		void Show();
	}
}
