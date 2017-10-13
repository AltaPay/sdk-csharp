using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;
using TransactionCardStatus = AltaPay.Service.Dto.TransactionCardStatus;
using AltaPay.Api.Tests;


namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class MerchantApiTests : BaseTest
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
			// _api = new MerchantApi("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword");
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			
			Assert.AreEqual(Result.Success, result.Result);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithFailParametersReturnsFailedResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 5.66, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Failed, result.Result);
			Assert.AreEqual("Card Declined", result.ResultMessage);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithErrorParametersReturnsErrorResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 5.67, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Error, result.Result);
			Assert.AreEqual("Internal Error", result.ResultMessage);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithAPaymentId(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.IsNotNull(result.Payment.TransactionId);
			Assert.IsTrue(result.Payment.TransactionId.Length > 0);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectShopOrderId(Boolean callReservationOfFixedAmount)
		{
			string orderid = Guid.NewGuid().ToString();
			PaymentResult result = GetMerchantApiResult(orderid, 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(orderid, result.Payment.ShopOrderId);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithAlternativeSourceReturnsSuccessfulResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, null, PaymentSource.eCommerce, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Success, result.Result);
		}



		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectTerminal(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("AltaPay Soap Test Terminal", result.Payment.Terminal);
		}


		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectReservedAmount(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(1.23, result.Payment.ReservedAmount);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectCapturedAmount(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			Assert.AreEqual(1.23, result.Payment.CapturedAmount);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithTheCorrectPaymentStatus(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("captured", result.Payment.TransactionStatus);
		}

		
		[TestCase(true)]
		[TestCase(false)]
		public void RefundingACapturedPayment(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("captured", result.Payment.TransactionStatus);

			result = _api.Refund(new RefundRequest(){
				PaymentId = result.Payment.PaymentId,
				OrderLines = new List<PaymentOrderLine>()
				{
					new PaymentOrderLine()
					{
						ItemId = "123456",
						Description = "Test product",
						UnitPrice = 24.33
					}
				}
			});

			Assert.AreEqual("refunded", result.Payment.TransactionStatus);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithACreditCardToken(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			
			Assert.NotNull(result.Payment.CreditCardToken);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithACreditCardMaskedPan(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual("411100******0002", result.Payment.CreditCardMaskedPan);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithSuccessfulParametersReturnsAPaymentWithCardStatus(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);

			Assert.AreEqual(TransactionCardStatus.Valid, result.Payment.CardStatus);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithCardTokenResultsInSuccessfullResult(Boolean callReservationOfFixedAmount)
		{
			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, callReservationOfFixedAmount);
			PaymentResult secondResult = GetMerchantApiResult(Guid.NewGuid().ToString(), 1.23, result.Payment.CreditCardToken, callReservationOfFixedAmount);

			Assert.AreEqual(Result.Success, secondResult.Result);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void CallingMerchantApiWithAvsInfoReturnsAvsResult(Boolean callReservationOfFixedAmount)
		{
			var customerInfo = new CustomerInfo();
			customerInfo.BillingAddress.Address="Albertslund";

			PaymentResult result = GetMerchantApiResult(Guid.NewGuid().ToString(), 3.34, customerInfo, callReservationOfFixedAmount);

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
		
		[Test]
        public void Capture_DoNotSendAmountIfNotSpecified()
        {
			/**
			 * This test does not really check anything, but it does
			 * send a request to the gateway, where you can then check
			 * that everything is as expected.
			 */
			
			var captureRequest = new CaptureRequest() {
				PaymentId =  "60"
			};
			PaymentResult result = _api.Capture(captureRequest);

			Console.Out.WriteLine("test: " + result.ResultMessage);
        }

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, CustomerInfo customerInfo, 
			Boolean callReservationOfFixedAmount)
		{

			return GetMerchantApiResult(shopOrderId, amount, customerInfo, PaymentSource.moto, callReservationOfFixedAmount);

		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, CustomerInfo customerInfo, 
			PaymentSource source = PaymentSource.moto, Boolean callReservationOfFixedAmount = true)
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

			if (callReservationOfFixedAmount) {
				return _api.Reserve(request); // reservation of fixed amount
			} 
			else {
				return _api.ReserveAmount(request); // reservation
			}

		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, Boolean callReservationOfFixedAmount = true)
		{
            DateTime sixMonthsFromNowDate = DateTime.Now.AddMonths(6);
			var request = new ReserveRequest {
				Terminal = "AltaPay Soap Test Terminal",
				ShopOrderId = shopOrderId,
				PaymentType = AuthType.paymentAndCapture,
				Amount = Amount.Get(amount, Currency.DKK),
				Pan = "4111000011110002",
				ExpiryMonth = sixMonthsFromNowDate.Month,
				ExpiryYear = sixMonthsFromNowDate.Year,
				Cvc = "123",
			};

			if (callReservationOfFixedAmount) {
				return _api.Reserve(request); // reservation of fixed amount
			} 
			else {
				return _api.ReserveAmount(request); // reservation
			}
		}

		private PaymentResult GetMerchantApiResult(string shopOrderId, double amount, string cardToken, Boolean callReservationOfFixedAmount = true)
		{
			var request = new ReserveRequest {
				Terminal = "AltaPay Soap Test Terminal",
				ShopOrderId = shopOrderId,
				PaymentType = AuthType.payment,
				Amount = Amount.Get(amount, Currency.DKK),
				CreditCardToken = cardToken,
				Cvc = "123",
			};

			if (callReservationOfFixedAmount) {
				return _api.Reserve(request); // reservation of fixed amount
			} 
			else {
				return _api.ReserveAmount(request); // reservation
			}
		}
	}
}
