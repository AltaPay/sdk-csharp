using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PensioMoto.Service;
using System.Diagnostics;


namespace PensioMoto.Tests.Integration
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
			paymentRequest.GetConfig().CallbackFormUrl         = "http://demoshop.pensio.com/Form";
			paymentRequest.GetConfig().CallbackOkUrl           = "http://demoshop.pensio.com/Ok";
			paymentRequest.GetConfig().CallbackFailUrl         = "http://demoshop.pensio.com/Fail";
			paymentRequest.GetConfig().CallbackRedirectUrl     = "http://demoshop.pensio.com/Redirect";
			paymentRequest.GetConfig().CallbackNotificationUrl = "http://demoshop.pensio.com/Notification";
			paymentRequest.GetConfig().CallbackOpenUrl         = "http://demoshop.pensio.com/Open";
			paymentRequest.GetConfig().CallbackVerifyOrderUrl  = "http://demoshop.pensio.com/VerifyOrder";
			
			// Fraud-service and params
			paymentRequest.GetConfig().FraudService = FraudService.test;
			paymentRequest.CustomerCreatedDate = "2010-12-24 21:00:00";
			
			// Customer Data
			paymentRequest.GetCustomerInfo().Email = "customer@email.com";
			paymentRequest.GetCustomerInfo().Username = "leatheruser";
			paymentRequest.GetCustomerInfo().CustomerPhone = "+4512345678";
			paymentRequest.GetCustomerInfo().BankName = "Gotham Bank";
			paymentRequest.GetCustomerInfo().BankPhone = "666 666 666";
			paymentRequest.GetCustomerInfo().GetBillingAddress().Address = "101 Night Street";
			paymentRequest.GetCustomerInfo().GetBillingAddress().City = "Gotham City";
			paymentRequest.GetCustomerInfo().GetBillingAddress().Country = "Bat Country";
			paymentRequest.GetCustomerInfo().GetBillingAddress().Firstname = "Bruce";
			paymentRequest.GetCustomerInfo().GetBillingAddress().Lastname = "Wayne";
			paymentRequest.GetCustomerInfo().GetBillingAddress().Region = "Dark Region";
			paymentRequest.GetCustomerInfo().GetShippingAddress().Address = "42 Joker Avenue";
			paymentRequest.GetCustomerInfo().GetShippingAddress().City = "Big Smile City";
			paymentRequest.GetCustomerInfo().GetShippingAddress().Country = "Laughistan";
			paymentRequest.GetCustomerInfo().GetShippingAddress().Firstname = "Jack";
			paymentRequest.GetCustomerInfo().GetShippingAddress().Lastname = "Napier";
			paymentRequest.GetCustomerInfo().GetShippingAddress().Region = "Umbrella Neighbourhood";
			
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
			paymentRequest.AddOrderLine("The Item Desc", "itemId1", 10, 10, "unitCode", 500, 0.00, GoodsType.item);
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
