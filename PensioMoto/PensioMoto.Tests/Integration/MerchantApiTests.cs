using System;
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
			_api.Initialize("http://gateway.dev.pensio.com/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal");
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

        [Test]
        public void WahNoaNoa()
        {
            PaymentDetails details = new PaymentDetails();
            
            details.AddOrderLine("Beautiful linen dress with print", "2-1099-1", 1, 0, "ROSEBUD", 274.5, 0, "item");
            details.AddOrderLine("Striped leggings", "2-1357-1", 1, 0, "LIGHT ARO", 74.5, 0, "item");
            details.AddOrderLine("Gorgeous tunic with brodery anglaise", "2-1135-1", 1, 0, "PURPLE", 214.5, 0, "item");
            details.AddOrderLine("Cool cotton trousers with stretch", "2-1127-1", 1, 0, "WALNUT", 249.5, 0, "item");
            details.AddOrderLine("Cotton dress with print and short slee", "2-1277-1", 1, 0, "DOE", 449, 0, "item");
            details.AddOrderLine("Striped, long-sleeved T-shirt", "2-0177-11", 1, 0, "CHALK", 99.5, 0, "item");
            details.AddOrderLine("Cool cotton trousers with stretch", "2-1127-1", 1, 0, "WALNUT", 249.5, 0, "item");
            details.AddOrderLine("T-shirt I many colours", "1-2766-1", 1, 0, "IBIS", 89.5, 0, "item");
            details.AddOrderLine("Striped leggings", "2-1357-1", 1, 0, "LIGHT ARO", 74.5, 0, "item");
            details.AddOrderLine("Pretty printed sleeveless cotton top", "2-1060-1", 1, 0, "PURPLE", 189.5, 0, "item");
            details.AddOrderLine("Feminine patterned cotton shirt", "2-0856-1", 1, 0, "STONE", 199.5, 0, "item");
            details.AddOrderLine("Striped, long-sleeved T-shirt", "2-0177-9", 1, 0, "LIGHT NEC", 99.5, 0, "item");
            details.AddOrderLine("Freight", "", 1, 0, "", 0, 0, "shipment");
            PaymentResult res = _api.Capture("59", 2039, details);

            Console.Out.WriteLine("test: " + res.ResultMessage);
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
