using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;
using TransactionCardStatus = AltaPay.Service.Dto.TransactionCardStatus;


namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class MerchantApiTests
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi("http://gateway.dev.pensio.com/merchant.php/API/", "shop api", "testpassword");
			// _api = new MerchantApi("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword");
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

			Assert.IsNotNull(result.Payment.TransactionId);
			Assert.IsTrue(result.Payment.TransactionId.Length > 0);
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectShopOrderId()
		{
			string orderid = Guid.NewGuid().ToString();
			PaymentResult result = GetMerchantApiResult(orderid, 1.23);

			Assert.AreEqual(orderid, result.Payment.ShopOrderId);
		}

		[Test]
		public void CallingMerchantApiWithAlternativeSourceReturnsSuccessfulResult()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23,null,PaymentSource.eCommerce);

			Assert.AreEqual(Result.Success, result.Result);
		}



		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectTerminal()
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23);

			Assert.AreEqual("AltaPay Soap Test Terminal", result.Payment.Terminal);
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

			Assert.AreEqual("captured", result.Payment.TransactionStatus);
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

			Assert.AreEqual(TransactionCardStatus.Valid, result.Payment.CardStatus);
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
			var customerInfo = new CustomerInfo();
			customerInfo.BillingAddress.Address="Albertslund";

			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 3.34, customerInfo);

			Assert.AreEqual("A", result.Payment.AddressVerification);
			Assert.AreEqual("Address matches, but zip code does not", result.Payment.AddressVerificationDescription);
		}

        [Test]
        public void NoaNoa_LongOrderLines()
        {

			var captureRequest = new CaptureRequest() {
				PaymentId =  "59",
				Amount = Amount.Get(2039, Currency.XXX),
				OrderLines = {
					new PaymentOrderLine() { Description = "Beautiful linen dress with print", ItemId = "2-1099-1", Quantity = 1, TaxPercent = 0, UnitCode = "ROSEBUD", UnitPrice = 274.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped leggings", ItemId = "2-1357-1", Quantity = 1, TaxPercent = 0, UnitCode = "LIGHT ARO", UnitPrice = 74.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Gorgeous tunic with brodery anglaise", ItemId = "2-1135-1", Quantity = 1, TaxPercent = 0, UnitCode = "PURPLE", UnitPrice = 214.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Cool cotton trousers with stretch", ItemId = "2-1127-1", Quantity = 1, TaxPercent = 0, UnitCode = "WALNUT", UnitPrice = 249.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Cotton dress with print and short slee", ItemId = "2-1277-1", Quantity = 1, TaxPercent = 0, UnitCode = "DOE", UnitPrice = 449, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped, long-sleeved T-shirt", ItemId = "2-0177-11", Quantity = 1, TaxPercent = 0, UnitCode = "CHALK", UnitPrice = 99.5 , Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Cool cotton trousers with stretch", ItemId = "2-1127-1", Quantity = 1, TaxPercent = 0, UnitCode = "WALNUT", UnitPrice = 249.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "T-shirt I many colours", ItemId = "1-2766-1", Quantity = 1, TaxPercent = 0, UnitCode = "IBIS", UnitPrice = 89.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped leggings", ItemId = "2-1357-1", Quantity = 1, TaxPercent = 0, UnitCode = "LIGHT ARO", UnitPrice = 74.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Pretty printed sleeveless cotton top", ItemId = "2-1060-1", Quantity = 1, TaxPercent = 0, UnitCode = "PURPLE", UnitPrice = 189.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Feminine patterned cotton shirt", ItemId = "2-0856-1", Quantity = 1, TaxPercent = 0, UnitCode = "STONE", UnitPrice = 199.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Striped, long-sleeved T-shirt", ItemId = "2-0177-9", Quantity = 1, TaxPercent = 0, UnitCode = "LIGHT NEC", UnitPrice = 199.5, Discount = 0, GoodsType = GoodsType.Item },
					new PaymentOrderLine() { Description = "Freight", ItemId = "", Quantity = 1, TaxPercent = 0, UnitCode = "", UnitPrice = 0, Discount = 0, GoodsType = GoodsType.Shipment },
				}
			};
			PaymentResult result = _api.Capture( captureRequest);

			Console.Out.WriteLine("test: " + result.ResultMessage);
        }

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, CustomerInfo customerInfo, PaymentSource source = PaymentSource.moto)
		{
			var request = new ReserveRequest {
				Source = source,
				ShopOrderId = shopOrderId,
				Terminal = "AltaPay Soap Test Terminal",
				PaymentType = AuthType.payment,
				Amount = Amount.Get(amount, Currency.DKK),
				Pan = "4111000011110002",
				ExpiryMonth = 1,
				ExpiryYear = 2018,
				Cvc = "123",
				CustomerInfo = customerInfo,
			};
			return _api.Reserve(request);
		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount)
		{
			var request = new ReserveRequest {
				Terminal = "AltaPay Soap Test Terminal",
				ShopOrderId = shopOrderId,
				PaymentType = AuthType.paymentAndCapture,
				Amount = Amount.Get(amount, Currency.DKK),
				Pan = "4111000011110002",
				ExpiryMonth = 1,
				ExpiryYear = 2018,
				Cvc = "123",
			};
			return _api.Reserve(request);
		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, string cardToken)
		{
			var request = new ReserveRequest {
				Terminal = "AltaPay Soap Test Terminal",
				ShopOrderId = shopOrderId,
				PaymentType = AuthType.payment,
				Amount = Amount.Get(amount, Currency.DKK),
				CreditCardToken = cardToken,
				Cvc = "123",
			};
			return _api.Reserve(request);
		}
	}
}
