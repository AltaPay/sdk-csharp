using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
	public interface IMerchantApi
	{
		PaymentResult ReservationOfFixedAmountMOTO(string shopOrderId,
			float amount,
			int currency,
			PaymentType paymentType,
			int pan,
			int expiryMonth,
			int expiryYear,
			int cvc);

		PaymentResult ReservationOfFixedAmountMOTO(string shopOrderId,
			float amount,
			int currency,
			PaymentType paymentType,
			string creditCardToken,
			int cvc);
	}
}
