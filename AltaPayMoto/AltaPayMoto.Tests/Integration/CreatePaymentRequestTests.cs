using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;
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
			PaymentRequest paymentRequest = new PaymentRequest() {
				Terminal = 	"AltaPay Soap Test Terminal",
				ShopOrderId = "payment-request-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(42.34,Currency.EUR),
			};
			
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
			PaymentRequest paymentRequest = new PaymentRequest() {
				Terminal = "AltaPay Soap Test Terminal",
				ShopOrderId = "payment-request-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(5056.93, Currency.EUR),

				// All the callback configs
				Config = new PaymentRequestConfig() {
					CallbackFormUrl         = "http://demoshop.pensio.com/Form",
					CallbackOkUrl           = "http://demoshop.pensio.com/Ok",
					CallbackFailUrl         = "http://demoshop.pensio.com/Fail",
					CallbackRedirectUrl     = "http://demoshop.pensio.com/Redirect",
					CallbackNotificationUrl = "http://demoshop.pensio.com/Notification",
					CallbackOpenUrl         = "http://demoshop.pensio.com/Open",
					CallbackVerifyOrderUrl  = "http://demoshop.pensio.com/VerifyOrder",
					FraudService = FraudService.Test,
				},
				
				// Customer Data
				CustomerInfo = {
					Email = "customer@email.com",
					Username = "leatheruser",
					CustomerPhone = "+4512345678",
					BankName = "Gotham Bank",
					BankPhone = "666 666 666",
					
					BillingAddress = new CustomerAddress() {
						Address = "101 Night Street",
						City = "Gotham City",
						Country = "Bat Country",
						Firstname = "Bruce",
						Lastname = "Wayne",
						Region = "Dark Region",
					},
					
					ShippingAddress = new CustomerAddress() {
						Address = "42 Joker Avenue",
						City = "Big Smile City",
						Country = "Laughistan",
						Firstname = "Jack",
						Lastname = "Napier",
						Region = "Umbrella Neighbourhood",
					}
				},

				// Many other optional parameters
				CustomerCreatedDate = "2010-12-24 21:00:00",
				Cookie = "thecookie=isgood",
				CreditCardToken = "424242424242424242424242",
				Language = "fr",
				OrganisationNumber = "Orgnumber42",
				SalesInvoiceNumber = "87654321",
				SalesReconciliationIdentifier = "sales_recon_id",
				SalesTax = 56.93,
				ShippingType = ShippingType.Military,
				AccountOffer = AccountOffer.disabled,
				Type = AuthType.payment,
				
				
				// Orderlines
				OrderLines = {
					new PaymentOrderLine() {
						Description = "The Item Desc", 
						ItemId = "itemId1",
						Quantity = 10,
						TaxPercent = 10,
						UnitCode = "unitCode",
						UnitPrice = 500,
						Discount = 0.00,
						GoodsType = GoodsType.Item,
					},
				},
				
				// Payment Infos
				PaymentInfos = {
					{"auxinfo1", "auxvalue1"},
				},
			};
			
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
