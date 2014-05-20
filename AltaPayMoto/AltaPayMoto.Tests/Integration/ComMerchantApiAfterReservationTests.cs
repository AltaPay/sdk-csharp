﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;
using AltaPay.Moto.Com;

namespace AltaPay.Moto.Tests.Integration
{
	[TestFixture]
	public class ComMerchantApiAfterReservationTests
	{
		IMerchant _api;
		MerchantApi _merchantApi;

		[SetUp]
		public void Setup()
		{
			_api = new Merchant();
			_api.Initialize("http://gateway.dev.pensio.com/merchant.php/API/",
				"integration api", "1234", "AltaPay Soap Test Terminal");

			_merchantApi = new MerchantApi();
			_merchantApi.Initialize("http://gateway.dev.pensio.com/merchant.php/API/",
				"integration api", "1234", "AltaPay Soap Test Terminal");
		}

		[Test]
		public void CapturePaymentWithOrderLinesReturnsSuccess()
		{
			//throw new Exception(ReserveAmount(1.23, PaymentType.payment).ResultMessage);
			IPaymentDetails paymentDetails = _api.CreatePaymentDetails();
			paymentDetails.AddOrderLine("Ninja", "N1", 1.0, 0.25, "kg", 100.00, 10, "item");
			paymentDetails.SalesTax = 12.34;
			PaymentResult r = ReserveAmount(1.23, AuthType.payment);
			
			IComPaymentResult result = _api.CaptureWithPaymentDetails(r.Transaction.TransactionId, 1.23, paymentDetails);

			Assert.AreEqual("Success", result.Result);
		}

		private PaymentResult ReserveAmount(double amount, AuthType type)
		{
			var request = new PaymentReservationRequest {
				ShopOrderId = "csharptest" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(amount, Currency.DKK),
				PaymentType = type,
				Pan = "4111000011110000", 
				ExpiryMonth = 1,
				ExpiryYear = 2012,
				Cvc = "123"
			};
			return _merchantApi.Reserve(request);
		}
	}
}