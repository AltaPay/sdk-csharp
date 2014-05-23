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

		ChargeSubscriptionResult ChargeSubscription(ChargeSubscriptionParam param);
		ReserveSubscriptionChargeResult ReserveSubscriptionCharge(ReserveSubscriptionChargeParam param);

		PaymentRequestResult CreatePaymentRequest(PaymentRequestParam param);
		ReserveResult Reserve(ReserveParam param);
		ReleaseResult Release(ReleaseParam param);
		CaptureResult Capture(CaptureParam param);
		RefundResult Refund(RefundParam request);
		GetPaymentResult GetPayment(GetPaymentParam param);

		FundingsResult GetFundings(GetFundingsParam param);

		ApiResult ParsePostBackXmlResponse(string responseStr);
		ApiResult ParsePostBackXmlResponse(Stream responseStr);
	}
}
