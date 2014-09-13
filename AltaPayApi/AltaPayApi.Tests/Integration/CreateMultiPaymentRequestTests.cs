using System;
using NUnit.Framework;
using AltaPay.Service;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class CreateMultiPaymentRequestTests : BaseTest
	{
		private const string gatewayUrl = "http://gateway.dev.pensio.com/merchant.php/API/";
		private const string username = "shop api";
		private const string password = "testpassword";
		private const string terminal = "AltaPay Soap Test Terminal";

		private IMerchantApi _api;


		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(gatewayUrl, username, password);
		}

		[Test]
		public void SimpleMultiPaymentRequest()
		{
			var paymentRequest = new MultiPaymentRequestRequest() {
				Terminal = 	terminal,
				ShopOrderId = "multi-payment-request-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(0, Currency.EUR),
			};

			paymentRequest.AddChild(new MultiPaymentRequestRequestChild() {
				Amount = Amount.Get(12.34m, Currency.EUR)
			});

			MultiPaymentRequestResult result = _api.CreateMultiPaymentRequest(paymentRequest);

			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(Result.Success, result.Result);
			Assert.IsNotEmpty(result.Url);
		}
	}
}
