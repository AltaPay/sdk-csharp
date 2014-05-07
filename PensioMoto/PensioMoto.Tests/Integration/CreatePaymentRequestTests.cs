using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using AltaPay.Service;
using System.Diagnostics;


namespace AltaPay.Moto.Tests.Integration
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

		[Test]
		public void CreateComplexPaymentRequest()
		{
			PaymentRequest paymentRequest = new PaymentRequest();
			paymentRequest.Terminal = "AltaPay Soap Test Terminal";
			paymentRequest.ShopOrderId = "payment-request-" + Guid.NewGuid().ToString();
			paymentRequest.Amount = 5056.93;
			paymentRequest.Currency = Currency.EUR;
			
			// All the callback configs
			paymentRequest.Config.CallbackFormUrl         = "http://demoshop.pensio.com/Form";
			paymentRequest.Config.CallbackOkUrl           = "http://demoshop.pensio.com/Ok";
			paymentRequest.Config.CallbackFailUrl         = "http://demoshop.pensio.com/Fail";
			paymentRequest.Config.CallbackRedirectUrl     = "http://demoshop.pensio.com/Redirect";
			paymentRequest.Config.CallbackNotificationUrl = "http://demoshop.pensio.com/Notification";
			paymentRequest.Config.CallbackOpenUrl         = "http://demoshop.pensio.com/Open";
			paymentRequest.Config.CallbackVerifyOrderUrl  = "http://demoshop.pensio.com/VerifyOrder";
			
			// Fraud-service and params
			paymentRequest.Config.FraudService = FraudService.Test;
			paymentRequest.CustomerCreatedDate = "2010-12-24 21:00:00";
			
			// Customer Data
			paymentRequest.CustomerInfo.Email = "customer@email.com";
			paymentRequest.CustomerInfo.Username = "leatheruser";
			paymentRequest.CustomerInfo.CustomerPhone = "+4512345678";
			paymentRequest.CustomerInfo.BankName = "Gotham Bank";
			paymentRequest.CustomerInfo.BankPhone = "666 666 666";
			paymentRequest.CustomerInfo.BillingAddress.Address = "101 Night Street";
			paymentRequest.CustomerInfo.BillingAddress.City = "Gotham City";
			paymentRequest.CustomerInfo.BillingAddress.Country = "Bat Country";
			paymentRequest.CustomerInfo.BillingAddress.Firstname = "Bruce";
			paymentRequest.CustomerInfo.BillingAddress.Lastname = "Wayne";
			paymentRequest.CustomerInfo.BillingAddress.Region = "Dark Region";
			paymentRequest.CustomerInfo.ShippingAddress.Address = "42 Joker Avenue";
			paymentRequest.CustomerInfo.ShippingAddress.City = "Big Smile City";
			paymentRequest.CustomerInfo.ShippingAddress.Country = "Laughistan";
			paymentRequest.CustomerInfo.ShippingAddress.Firstname = "Jack";
			paymentRequest.CustomerInfo.ShippingAddress.Lastname = "Napier";
			paymentRequest.CustomerInfo.ShippingAddress.Region = "Umbrella Neighbourhood";
			
			// Many other optional parameters
			paymentRequest.Cookie = "thecookie=isgood";
			paymentRequest.CreditCardToken = "424242424242424242424242";
			paymentRequest.Language = "fr";
			paymentRequest.OrganisationNumber = "Orgnumber42";
			paymentRequest.SalesInvoiceNumber = "87654321";
			paymentRequest.SalesReconciliationIdentifier = "sales_recon_id";
			paymentRequest.SalesTax = 56.93;
			paymentRequest.ShippingType = ShippingType.Military;
			paymentRequest.AccountOffer = AccountOffer.disabled;
			paymentRequest.Type = PaymentType.payment;
			
			// Orderlines and TransactionInfo
			paymentRequest.AddOrderLine("The Item Desc", "itemId1", 10, 10, "unitCode", 500, 0.00, GoodsType.Item);
			paymentRequest.AddInfo("auxinfo1", "auxvalue1");
			
			// And make the actual invocation.
			PaymentRequestResult result = _api.CreatePaymentRequest(paymentRequest);
			
			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(Result.Success, result.Result);
			Assert.IsNotEmpty(result.Url);
			Assert.IsNotEmpty(result.DynamicJavascriptUrl);
			
			// System.Diagnostics.Process.Start(result.Url);
		}
	}
}
