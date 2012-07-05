﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PensioMoto.Service;

namespace PensioMoto.Tests.Integration
{
	[TestFixture]
	public class MerchantApiTests
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi();
			//_api.Initialize("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal");
			_api.Initialize("http://10.101.97.14/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal");
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CallingMerchantApiWithFailParametersReturnsFailedResult()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 5.66);

			Assert.AreEqual(Result.Failed, result.Result);
			Assert.AreEqual("Card Declined", result.ResultMessage);
		}

		[Test]
		public void CallingMerchantApiWithErrorParametersReturnsErrorResult()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 5.67);

			Assert.AreEqual(Result.Error, result.Result);
			Assert.AreEqual("Expected System Error", result.ResultMessage);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithAPaymentId()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.IsNotNull(result.Payment.PaymentId);
			Assert.IsTrue(result.Payment.PaymentId.Length > 0);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectShopOrderId()
		{
			string orderid = Guid.NewGuid().ToString();
			PaymentResult result = GetMerchantApiResult(orderid, 1.23);

			Assert.AreEqual(orderid, result.Payment.ShopOrderId);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectTerminal()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual("Pensio Test Terminal", result.Payment.Terminal);
		}


		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectReservedAmount()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual(1.23, result.Payment.ReservedAmount);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectCapturedAmount()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual(1.23, result.Payment.CapturedAmount);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectPaymentStatus()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual("captured", result.Payment.PaymentStatus);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithACreditCardToken()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);
			
			Assert.NotNull(result.Payment.CreditCardToken);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithACreditCardMaskedPan()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual("411100******0002", result.Payment.CreditCardMaskedPan);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithCardStatus()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual("Valid", result.Payment.CardStatus);
		}

		[Test]
		public void CallingMerchantApiWithCardTokenResultsInSuccessfullResult()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);
			PaymentResult secondResult = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, result.Payment.CreditCardToken);

			Assert.AreEqual(Result.Success, secondResult.Result);
		}

		[Test]
		public void CallingMerchantApiWithAvsInfoReturnsAvsResult()
		{
			AvsInfo avsInfo = new AvsInfo
			{
				Address = "Albertslund"
			};

			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 3.34, avsInfo);

			Assert.AreEqual("A", result.Payment.AddressVerification);
			Assert.AreEqual("Address matches, but zip code does not", result.Payment.AddressVerificationDescription);
		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, AvsInfo avsInfo)
		{
			return _api.ReservationOfFixedAmountMOTO(shopOrderId, amount, 208, PaymentType.payment, "4111000011110002", 1, 2018, "123", avsInfo);
		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount)
		{
			return _api.ReservationOfFixedAmountMOTO(shopOrderId, amount, 208, PaymentType.paymentAndCapture, "4111000011110002", 1, 2018, "123", null);
		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, string cardToken)
		{
			return _api.ReservationOfFixedAmountMOTO(shopOrderId, amount, 208, PaymentType.payment, cardToken, "123", null);
		}
	}
}
