using System;
using System.IO;

namespace AltaPay.Service
{
	public interface IMerchantApi
	{
		ChargeSubscriptionResult ChargeSubscription(ChargeSubscriptionRequest request);
		ReserveSubscriptionChargeResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request);

		PaymentRequestResult CreatePaymentRequest(PaymentRequestRequest request);
		MultiPaymentRequestResult CreateMultiPaymentRequest(MultiPaymentRequestRequest request);
		ReserveResult Reserve(ReserveRequest request);
		ReleaseResult Release(ReleaseRequest request);
		CaptureResult Capture(CaptureRequest request);
		RefundResult Refund(RefundRequest request);
		GetPaymentResult GetPayment(GetPaymentRequest request);
		GetPaymentsResult GetPayments(GetPaymentsRequest request);

		FundingsResult GetFundings(GetFundingsRequest request);

		ApiResult ParsePostBackXmlResponse(string responseStr);
		ApiResult ParsePostBackXmlResponse(Stream responseStr);
		MultiPaymentApiResult ParseMultiPaymentPostBackXmlResponse(string responseStr);
		MultiPaymentApiResult ParseMultiPaymentPostBackXmlResponse(Stream responseStream);
	}
}
