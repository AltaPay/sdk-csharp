using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
	public interface IMerchantApi
	{
		void Initialize(string gatewayUrl, string terminal, string username, string password);

		PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId,
			double amount,
			int currency,
			PaymentType paymentType,
			string pan,
			int expiryMonth,
			int expiryYear,
			string cvc);

		PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId,
			double amount,
			int currency,
			PaymentType paymentType,
			string creditCardToken,
			string cvc);

		PaymentResult Capture(string paymentId, double amount);
		PaymentResult Refund(string paymentId, double amount);
		PaymentResult Release(string paymentId);
		PaymentResult Split(string paymentId, double amount);
		PaymentResult GetPayment(string paymentId);
		PaymentResult CaptureRecurring(string recurringPaymentId, double amount);
		PaymentResult PreauthRecurring(string recurringPaymentId, double amount);
	}
}
