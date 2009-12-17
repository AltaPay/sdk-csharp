using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	public interface IMoto
	{
		void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal, string orderId, double amount, int currency, string paymentType);

		void AddCreditCard(string maskedPan, string cardToken);

		IComPaymentResult Show();
	}
}
