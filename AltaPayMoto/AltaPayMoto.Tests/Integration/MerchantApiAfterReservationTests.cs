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
			var reserveResult = ReserveAmount(1.23, AuthType.payment);

			var captureParam = new CaptureParam() {
				PaymentId =  reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.23, Currency.DKK),
			};
			PaymentResult result = _api.Capture( captureParam);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void CapturePaymentWithOrderLinesReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			if (reserveResult.Result != Result.Success)
				throw new Exception(reserveResult.ResultMessage);

			var captureParam = new CaptureParam() {
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
			PaymentResult result = _api.Capture( captureParam);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void RefundPaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			var refundParam = new RefundParam() {
				PaymentId = reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.23, Currency.XXX),
			};
			Assert.AreEqual(Result.Success, _api.Refund(refundParam).Result);
		}

		[Test]
		public void RefundPaymentReturnsRefundedAmount()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.paymentAndCapture);
			var refundParam = new RefundParam() {
				PaymentId = reserveResult.Transaction.TransactionId,
				Amount = Amount.Get(1.11, Currency.DKK),
			};
			Assert.AreEqual(1.11, _api.Refund(refundParam).Transaction.RefundedAmount);
		}

		[Test]
		public void ReleasePaymentReturnsSuccess()
		{
			var reserveResult = ReserveAmount(1.23, AuthType.payment);
			var releaseParam = new ReleaseParam {
				PaymentId = reserveResult.Transaction.TransactionId,
			};
			PaymentResult result = _api.Release(releaseParam);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void GetPaymentReturnsPayment()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.payment);
			PaymentResult result = _api.GetPayment(new GetPaymentParam { PaymentId = createPaymentResult.Transaction.TransactionId} );

			Assert.AreEqual(createPaymentResult.Transaction.TransactionId, result.Transaction.TransactionId);
		}

		[Test]
		public void GetNonExistingPaymentReturnsNullPayment()
		{
			PaymentResult result = _api.GetPayment(new GetPaymentParam { PaymentId = "-1"});

			Assert.IsNull(result.Transaction);
		}

		[Test]
		public void ChargeSubscriptionReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var chargeSubscriptionParam = new ChargeSubscriptionParam() {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ChargeSubscription(chargeSubscriptionParam);

			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void ChargeSubscriptionReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var chargeSubscriptionParam = new ChargeSubscriptionParam() {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ChargeSubscription(chargeSubscriptionParam);

			Assert.AreEqual(createPaymentResult.Transaction.TransactionId, result.Transaction.TransactionId);
			Assert.AreEqual("recurring_confirmed", result.Transaction.TransactionStatus);
			Assert.AreEqual("captured", result.RecurringPayment.TransactionStatus);
		}

		[Test]
		public void ReserveSubscriptionChargeReturnsSuccess()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var reserveSubscriptionParam = new ReserveSubscriptionChargeParam {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ReserveSubscriptionCharge(reserveSubscriptionParam);
			Assert.AreEqual(Result.Success, result.Result);
		}

		[Test]
		public void ReserveSubscriptionChargeReturnsBothPayments()
		{
			PaymentResult createPaymentResult = ReserveAmount(1.23, AuthType.subscription);
			var reserveSubscriptionParam = new ReserveSubscriptionChargeParam {
				SubscriptionId = createPaymentResult.Transaction.TransactionId,
				Amount = Amount.Get(1, Currency.XXX),
			};
			SubscriptionResult result = _api.ReserveSubscriptionCharge(reserveSubscriptionParam);

			Assert.AreEqual(createPaymentResult.Transaction.TransactionId, result.Transaction.TransactionId);
			Assert.AreEqual("recurring_confirmed", result.Transaction.TransactionStatus);
			Assert.AreEqual("preauth", result.RecurringPayment.TransactionStatus);
		}

		private PaymentResult ReserveAmount(double amount, AuthType type)
		{
			var reservePaymentParam = new ReserveParam {
				ShopOrderId = "csharptest"+Guid.NewGuid().ToString(),
				Amount = Amount.Get(amount, Currency.DKK),
				PaymentType = type,
				Pan = "4111000011110000",
				ExpiryMonth = 1,
				ExpiryYear = 2012,
				Cvc = "123",
			};

			PaymentResult result = _api.Reserve(reservePaymentParam);
			
			if(result.Result != Result.Success)
			{
				throw new Exception("The result was: "+result.Result+", message: "+result.ResultMerchantMessage);
			}
			
			return result;
		}
	}
}
