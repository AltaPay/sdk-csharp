using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto.Service
{
	public interface IMerchantApi
	{
		void Initialize(string gatewayUrl, string username, string password, string terminal);

		PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId,
			double amount,
			int currency,
			PaymentType paymentType,
			string pan,
			int expiryMonth,
			int expiryYear,
			string cvc,
			AvsInfo avsInfo);

		PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId,
			double amount,
			int currency,
			PaymentType paymentType,
			string creditCardToken,
			string cvc,
			AvsInfo avsInfo);

		PaymentResult Capture(string paymentId, double amount);
		PaymentResult Refund(string paymentId, double amount);
		PaymentResult Release(string paymentId);
		SplitPaymentResult Split(string paymentId, double amount);
		PaymentResult GetPayment(string paymentId);
		RecurringResult CaptureRecurring(string recurringPaymentId, double amount);
		RecurringResult PreauthRecurring(string recurringPaymentId, double amount);

		FundingsResult getFundings(int page);
	}
}
