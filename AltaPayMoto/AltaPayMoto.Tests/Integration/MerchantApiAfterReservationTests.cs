﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;

namespace AltaPay.Moto.Tests.Integration
{
	[TestFixture]
	public class MerchantApiAfterReservationTests
	{
		MerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi();
			_api.Initialize("http://gateway.dev.pensio.com/merchant.php/API/",
				"integration api", "1234", "AltaPay Soap Test Terminal");
		}

		[Test]
		public void CapturePaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);

			var request = new CaptureRequest() {
				PaymentId =  reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.23, Currency.DKK),
			};
			PaymentResult result = _api.Capture( request);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CapturePaymentWithOrderLinesReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			if (reserveResult.Result != Result.Success)
				throw new Exception(reserveResult.ResultMessage);

			var request = new CaptureRequest() {
				PaymentId =  reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.23, Currency.DKK),
				OrderLines = {
					new PaymentOrderLine() {
						Description = "Ninja",
						ItemId = "N1",
						Quantity = 1.0,
						TaxPercent = 0.25,
						UnitCode = "kg",
						UnitPrice = 100.00,
						Discount = 10,
						GoodsType = GoodsType.Item
					}
				}
			};
			PaymentResult result = _api.Capture( request);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void RefundPaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			var request = new RefundRequest() {
				PaymentId = reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.23, Currency.XXX),
			};
			Assert.AreEqual(Result.Success, _api.Refund(request).Result);
		}

		[Test]
		public void RefundPaymentReturnsRefundedAmount()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			var request = new RefundRequest() {
				PaymentId = reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.11, Currency.DKK),
			};
			Assert.AreEqual(1.11, _api.Refund(request).Transaction.RefundedAmount);
		}

		[Test]
		public void ReleasePaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			var request = new ReleaseRequest {
				PaymentId = reserveResult.Transaction.TransactionId,
			};
			PaymentResult result = _api.Release(request);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void GetPaymentReturnsPayment()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			PaymentResult result = _api.GetPayment(createPaymentResult.Transaction.TransactionId);

			Assert.AreEqual(createPaymentResult.Transaction.TransactionId, result.Transaction.TransactionId);
		}

		[Test]
		public void GetNonExistingPaymentReturnsNullPayment()
		{
			PaymentResult result = _api.GetPayment("-1");

			Assert.IsNull(result.Transaction);
		}
		
		/*
		[Test]
		public void SplitPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.TransactionId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}
		[Test]
		public void SplitPaymentReturnsTwoPaymentWhenSplitFails()
		{
			PaymentResult createPaymentResult = ReserveAmount(42, AuthType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.TransactionId, 10.66);

			Assert.AreEqual(Result.Failed, result.Result);
			
			Assert.IsNotNull(result.Payment);
			Assert.IsNotNull(result.SplitPayment1);
			Assert.IsNull(result.SplitPayment2);
		}

		[Test]
		public void SplitPaymentReturnsAllThreePayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.TransactionId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.TransactionId, result.Payment.PaymentId);
			Assert.AreEqual("split", result.Payment.PaymentStatus);
			Assert.AreEqual("preauth", result.SplitPayment1.PaymentStatus);
			Assert.AreEqual("preauth", result.SplitPayment2.PaymentStatus);
		}

		 */


		[Test]
		public void ChargeSubscriptionReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var request = new ChargeSubscriptionRequest() {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			RecurringResult result = _api.ChargeSubscription(request);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CaptureRecurringReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var request = new ChargeSubscriptionRequest() {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			RecurringResult result = _api.ChargeSubscription(request);

			Assert.AreEqual(createPaymentResult.Transaction.TransactionId, result.Transaction.TransactionId);
			Assert.AreEqual("recurring_confirmed", result.Transaction.TransactionStatus);
			Assert.AreEqual("captured", result.RecurringPayment.TransactionStatus);
		}

		[Test]
		public void PreauthReccurringPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var request = new ReserveSubscriptionChargeRequest {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			RecurringResult result = _api.ReserveSubscriptionCharge(request);
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void PreauthRecurringReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var request = new ReserveSubscriptionChargeRequest {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			RecurringResult result = _api.ReserveSubscriptionCharge(request);

			Assert.AreEqual(createPaymentResult.Transaction.TransactionId, result.Transaction.TransactionId);
			Assert.AreEqual("recurring_confirmed", result.Transaction.TransactionStatus);
			Assert.AreEqual("preauth", result.RecurringPayment.TransactionStatus);
		}

		private PaymentResult ReserveAmount(double amount, AuthType type)
		{
			var request = new PaymentReservationRequest {
				ShopOrderId = "csharptest"+Guid.NewGuid().ToString(),
				Amount = Amount.Get(amount, Currency.DKK),
				PaymentType = type,
				Pan = "4111000011110000",
				ExpiryMonth = 1,
				ExpiryYear = 2012,
				Cvc = "123",
			};

			PaymentResult result = _api.Reserve(request);
			
			if(result.Result != Result.Success)
			{
				throw new Exception("The result was: "+result.Result+", message: "+result.ResultMerchantMessage);
			}
			
			return result;
		}
	}
}