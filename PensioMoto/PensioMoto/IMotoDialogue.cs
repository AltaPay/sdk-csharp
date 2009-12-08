using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;

namespace PensioMoto
{
	public interface IMotoDialogue
	{
		void Initialize(string gatewayUrl, string apiUsername, string apiPassword,
			string terminal, string orderId,
			float amount, int currency, PaymentType paymentType);
		void SetCreditCard(string maskedPan, string cardToken);
		PaymentResult Show();
	}
}
