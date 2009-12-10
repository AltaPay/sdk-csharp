using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
	public interface IMerchantApi
	{
		void Initialize(string gatewayUrl, string terminal, string username, string password);

		PaymentResult ReservationOfFixedAmountMOTO(string shopOrderId,
			double amount,
			int currency,
			PaymentType paymentType,
			string pan,
			int expiryMonth,
			int expiryYear,
			int cvc);

		PaymentResult ReservationOfFixedAmountMOTO(string shopOrderId,
			double amount,
			int currency,
			PaymentType paymentType,
			string creditCardToken,
			int cvc);
	}
}
