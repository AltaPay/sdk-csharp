using System;
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
			//throw new Exception(ReserveAmount(1.23, PaymentType.payment).ResultMessage);
			PaymentResult result = _api.Capture(ReserveAmount(1.23, AuthType.payment).Payment.PaymentId, 1.23);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CapturePaymentWithOrderLinesReturnsSuccess()
		{
			//throw new Exception(ReserveAmount(1.23, PaymentType.payment).ResultMessage);
			PaymentDetails orderLines = new PaymentDetails();
			orderLines.AddOrderLine("Ninja", "N1", 1.0, 0.25, "kg", 100.00, 10, "item");
			PaymentResult r = ReserveAmount(1.23, AuthType.payment);

			if (r.Result != Result.Success)
			{
				throw new Exception(r.ResultMessage);
			}
			
			PaymentResult result = _api.Capture(r.Payment.PaymentId, 1.23, orderLines);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void RefundPaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			var request = new RefundRequest() {
				PaymentId = reserveResult.Payment.PaymentId,
				Amount = Amount.Get(1.23, Currency.XXX),
			};
			Assert.AreEqual(Result.Success, _api.Refund(request).Result);
		}

		[Test]
		public void RefundPaymentReturnsRefundedAmount()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			var request = new RefundRequest() {
				PaymentId = reserveResult.Payment.PaymentId,
				Amount = Amount.Get(1.11, Currency.DKK),
			};
			Assert.AreEqual(1.11, _api.Refund(request).Payment.RefundedAmount);
		}

		[Test]
		public void ReleasePaymentReturnsSuccess()
		{
			PaymentResult result = _api.Release(ReserveAmount(1.23, AuthType.payment).Payment.PaymentId);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void GetPaymentReturnsPayment()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			PaymentResult result = _api.GetPayment(createPaymentResult.Payment.PaymentId);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
		}

		[Test]
		public void GetNonExistingPaymentReturnsNullPayment()
		{
			PaymentResult result = _api.GetPayment("-1");

			Assert.IsNull(result.Payment);
		}

		[Test]
		public void SplitPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void SplitPaymentReturnsTwoPaymentWhenSplitFails()
		{
			PaymentResult createPaymentResult = ReserveAmount(42, AuthType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.PaymentId, 10.66);

			Assert.AreEqual(Result.Failed, result.Result);
			
			Assert.IsNotNull(result.Payment);
			Assert.IsNotNull(result.SplitPayment1);
			Assert.IsNull(result.SplitPayment2);
		}

		[Test]
		public void SplitPaymentReturnsAllThreePayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			SplitPaymentResult result = _api.Split(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
			Assert.AreEqual("split", result.Payment.PaymentStatus);
			Assert.AreEqual("preauth", result.SplitPayment1.PaymentStatus);
			Assert.AreEqual("preauth", result.SplitPayment2.PaymentStatus);
		}

		[Test]
		public void CaptureReccurringPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			RecurringResult result = _api.CaptureRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CaptureRecurringReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			RecurringResult result = _api.CaptureRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
			Assert.AreEqual("recurring_confirmed", result.Payment.PaymentStatus);
			Assert.AreEqual("captured", result.RecurringPayment.PaymentStatus);
		}

		[Test]
		public void PreauthReccurringPaymentReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			RecurringResult result = _api.PreauthRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void PreauthRecurringReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			RecurringResult result = _api.PreauthRecurring(createPaymentResult.Payment.PaymentId, 1.00);

			Assert.AreEqual(createPaymentResult.Payment.PaymentId, result.Payment.PaymentId);
			Assert.AreEqual("recurring_confirmed", result.Payment.PaymentStatus);
			Assert.AreEqual("preauth", result.RecurringPayment.PaymentStatus);
		}

		private PaymentResult ReserveAmount(double amount, AuthType type)
		{
			PaymentResult result = _api.ReservationOfFixedAmountMOTO("csharptest"+Guid.NewGuid().ToString(),
				amount, 208, type, "4111000011110000", 1, 2012, "123", null);
			
			if(result.Result != Result.Success)
			{
				throw new Exception("The result was: "+result.Result+", message: "+result.ResultMerchantMessage);
			}
			
			return result;
		}
	}
}
