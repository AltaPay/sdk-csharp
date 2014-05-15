using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaPay.Service
{
	public interface IMerchantApi
	{
		void Initialize(string gatewayUrl, string username, string password, string terminal);
		
		PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId,
			double amount,
			int currency,
			AuthType paymentType,
			string pan,
			int expiryMonth,
			int expiryYear,
			string cvc,
			AvsInfo avsInfo);

		PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId,
			double amount,
			int currency,
			AuthType paymentType,
			string creditCardToken,
			string cvc,
			AvsInfo avsInfo);

		PaymentResult Capture(CaptureRequest request);
		PaymentResult Refund(RefundRequest request);
		PaymentResult Release(ReleaseRequest request);
		PaymentResult GetPayment(string paymentId);
		PaymentRequestResult CreatePaymentRequest(PaymentRequest Request);
		RecurringResult ChargeSubscription(ChargeSubscriptionRequest request);

		// Note: 	PreauthRecurring-> RecurringResult ReserveSubscriptionCharge(string subscriptionId, double amount);
		RecurringResult PreauthRecurring(string recurringPaymentId, double amount);
		

		FundingsResult getFundings(int page);

		
	}
}
