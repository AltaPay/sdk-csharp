using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;
using System.IO;

namespace AltaPay.Service
{
	public interface IMerchantApi
	{
		ChargeSubscriptionResult ChargeSubscription(ChargeSubscriptionRequest request);
		ReserveSubscriptionChargeResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request);

		PaymentRequestResult CreatePaymentRequest(PaymentRequestRequest request);
		ReserveResult Reserve(ReserveRequest request);
		ReleaseResult Release(ReleaseRequest request);
		CaptureResult Capture(CaptureRequest request);
		RefundResult Refund(RefundRequest request);
		GetPaymentResult GetPayment(GetPaymentRequest request);
		GetPaymentsResult GetPayments(GetPaymentsRequest request);

		FundingsResult GetFundings(GetFundingsRequest request);

		ApiResult ParsePostBackXmlResponse(string responseStr);
		ApiResult ParsePostBackXmlResponse(Stream responseStr);
	}
}
