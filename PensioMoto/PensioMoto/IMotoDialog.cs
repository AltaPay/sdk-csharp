using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	public interface IMotoDialog
	{
		void Initialize(string gatewayUrl, string apiUsername, string apiPassword,
			string terminal, string orderId,
			double amount, int currency, PaymentType paymentType);
		void SetCreditCard(string maskedPan, string cardToken);
		PaymentResult Show();
	}
}
