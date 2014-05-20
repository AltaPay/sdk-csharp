using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaPay.Service
{
	public interface IMerchantApi
	{
		void Initialize(string gatewayUrl, string username, string password, string terminal);

		PaymentResult Reserve(PaymentReservationRequest request);
		PaymentResult Capture(CaptureRequest request);
		PaymentResult Refund(RefundRequest request);
		PaymentResult Release(ReleaseRequest request);
		PaymentResult GetPayment(GetPaymentRequest request);
		PaymentRequestResult CreatePaymentRequest(PaymentRequest Request);
		RecurringResult ChargeSubscription(ChargeSubscriptionRequest request);
		RecurringResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request);
		FundingsResult GetFundings(GetFundingsRequest request);
	}
}
