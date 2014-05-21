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
		void Initialize(string gatewayUrl, string username, string password, string terminal);

		RecurringResult ChargeSubscription(ChargeSubscriptionRequest request);
		RecurringResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request);

		PaymentRequestResult CreatePaymentRequest(PaymentRequest Request);
		PaymentResult Reserve(PaymentReservationRequest request);
		PaymentResult Release(ReleaseRequest request);
		PaymentResult Capture(CaptureRequest request);
		PaymentResult Refund(RefundRequest request);
		PaymentResult GetPayment(GetPaymentRequest request);

		FundingsResult GetFundings(GetFundingsRequest request);

		ApiResult ParsePostBackXmlResponse(string responseStr);
		ApiResult ParsePostBackXmlResponse(Stream responseStr);
	}
}
