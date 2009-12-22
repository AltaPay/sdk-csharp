﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PensioMoto.Service;

namespace PensioMoto.Tests.Integration
{
	[TestFixture]
	public class MerchantApiAfterReservationTests
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi();
			_api.Initialize("http://gateway.testserver.pensio.com/merchant.php/API/", 
				"Pensio Test Terminal", "shop api", "testpassword");
		}

		[Test]
		public void CapturePaymentReturnsSuccess()
		{
			PaymentResult result = _api.Capture(ReserveAmount(1.23, PaymentType.payment).Payment.PaymentId, 1.23);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void RefundPaymentReturnsSuccess()
		{
			PaymentResult result = _api.Refund(ReserveAmount(1.23, PaymentType.paymentAndCapture).Payment.PaymentId, 1.23);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void ReleasePaymentReturnsSuccess()
		{
			PaymentResult result = _api.Release(ReserveAmount(1.23, PaymentType.payment).Payment.PaymentId);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void GetPaymentReturnsPayment()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.payment);
			PaymentResult result = _api.GetPayment(createPaymentResult.Payment.PaymentId);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
		}

		[Test]
		public void SplitPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void SplitPaymentReturnsAllThreePayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
			Assert.AreEqual("split", result.Payment.PaymentStatus);
			Assert.AreEqual("preauth", result.SplitPayment1.PaymentStatus);
			Assert.AreEqual("preauth", result.SplitPayment2.PaymentStatus);
		}

		[Test]
		public void CaptureReccurringPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.recurring);
			RecurringResult result = _api.CaptureRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CaptureRecurringReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.recurring);
			RecurringResult result = _api.CaptureRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
			Assert.AreEqual("recurring_confirmed", result.Payment.PaymentStatus);
			Assert.AreEqual("captured", result.RecurringPayment.PaymentStatus);
		}

		[Test]
		public void PreauthReccurringPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.recurring);
			RecurringResult result = _api.PreauthRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void PreauthRecurringReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, PaymentType.recurring);
			RecurringResult result = _api.PreauthRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
			Assert.AreEqual("recurring_confirmed", result.Payment.PaymentStatus);
			Assert.AreEqual("preauth", result.RecurringPayment.PaymentStatus);
		}

		private PaymentResult ReserveAmount(double amount, PaymentType type)
		{
			return _api.ReservationOfFixedAmountMOTO("csharptest"+Guid.NewGuid().ToString(),
				amount, 208, type, "4111000011110000", 1, 2012, "123");
		}
	}
}
