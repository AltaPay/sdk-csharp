using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PensioMoto.Service;
using System.Diagnostics;


namespace PensioMoto.Tests.Integration
{
	[TestFixture]
	public class CreatePaymentRequestTests
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi();
			_api.Initialize("http://gateway.dev.pensio.com/merchant.php/API/", "shop api", "testpassword", "AltaPay Soap Test Terminal");
		}

		[Test]
		public void CreateSimplePaymentRequest()
		{
			PaymentRequest paymentRequest = new PaymentRequest();
			paymentRequest.Terminal = "AltaPay Soap Test Terminal";
			paymentRequest.ShopOrderId = "payment-request-" + Guid.NewGuid().ToString();
			paymentRequest.Amount = 42.34;
			paymentRequest.Currency = Currency.EUR;
			PaymentRequestResult result = _api.CreatePaymentRequest(paymentRequest);
			
			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(Result.Success, result.Result);
			Assert.IsNotEmpty(result.Url);
			Assert.IsNotEmpty(result.DynamicJavascriptUrl);
			
			//System.Diagnostics.Process.Start(result.Url);
		}
	}
}
