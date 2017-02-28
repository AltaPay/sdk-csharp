﻿using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;
using AltaPay.Service;
using System.Diagnostics;
using AltaPay.Service.Dto;
using System.IO;
using System.Net;
using AltaPay.Api.Tests;


namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	public class CreatePaymentRequestTests : BaseTest
	{

		private ParameterHelper ParameterHelper = new ParameterHelper();

		private IMerchantApi _api;


		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void CreateSimplePaymentRequest()
		{
			PaymentRequestRequest paymentRequest = new PaymentRequestRequest() {
				Terminal = 	GatewayConstants.terminal,
				ShopOrderId = "payment-req-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(42.34,Currency.EUR),
			};
			
			PaymentRequestResult result = _api.CreatePaymentRequest(paymentRequest);
			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(Result.Success, result.Result);
			Assert.IsNotEmpty(result.Url);
			Assert.IsNotEmpty(result.DynamicJavascriptUrl);
			Assert.IsNotEmpty(result.PaymentRequestId);
			
			//System.Diagnostics.Process.Start(result.Url);
		}

		[Test]
		public void CreateComplexPaymentRequest()
		{
			PaymentRequestRequest paymentRequest = new PaymentRequestRequest() {
				Terminal = GatewayConstants.terminal,
				ShopOrderId = "payment-req-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(5056.93, Currency.EUR),
				FraudService = FraudService.Test,

				// All the callback configs
				Config = new PaymentRequestConfig() {
					CallbackFormUrl         = "http://demoshop.pensio.com/Form",
					CallbackOkUrl           = "http://demoshop.pensio.com/Ok",
					CallbackFailUrl         = "http://demoshop.pensio.com/Fail",
					CallbackRedirectUrl     = "http://demoshop.pensio.com/Redirect",
					CallbackNotificationUrl = "http://demoshop.pensio.com/Notification",
					CallbackOpenUrl         = "http://demoshop.pensio.com/Open",
					CallbackVerifyOrderUrl  = "http://demoshop.pensio.com/VerifyOrder"
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
						Country = "US",
						Firstname = "Bruce",
						Lastname = "Wayne",
						Region = "Dark Region",
					},
					
					ShippingAddress = new CustomerAddress() {
						Address = "42 Joker Avenue",
						City = "Big Smile City",
						Country = "FI",
						Firstname = "Jack",
						Lastname = "Napier",
						Region = "Umbrella Neighbourhood",
					}
				},

				// Many other optional parameters
				CustomerCreatedDate = "2010-12-24",
				Cookie = "thecookie=isgood",
				//CreditCardToken = "424242424242424242424242",
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
			Assert.IsNotEmpty(result.PaymentRequestId);

			// System.Diagnostics.Process.Start(result.Url);
		}
		
		[Test]
		public void DoNotSendBothTaxAmountAndTaxPercent()
		{
			PaymentRequestRequest paymentRequest = new PaymentRequestRequest() {
				Terminal = GatewayConstants.terminal,
				ShopOrderId = "payment-req-" + Guid.NewGuid().ToString(),
				Amount = Amount.Get(5056.93, Currency.EUR),
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
				}
			};
			
			// And make the actual invocation.
			PaymentRequestResult result = _api.CreatePaymentRequest(paymentRequest);
			
			Assert.AreEqual(null, result.ResultMerchantMessage);
			Assert.AreEqual(Result.Success, result.Result);
			Assert.IsNotEmpty(result.Url);
			Assert.IsNotEmpty(result.DynamicJavascriptUrl);
			Assert.IsNotEmpty(result.PaymentRequestId);
		}

		[Test]
		public void ParseCallbackParameters() 
		{

			// Make call to reserve fixed amount
			var parameters = new Dictionary<string, object>();

			parameters.Add("terminal", GatewayConstants.terminal);
			parameters.Add("shop_orderid",  "shop api");
			parameters.Add("amount", "123.45");
			parameters.Add("currency", Currency.DKK.GetNumericString());

			parameters.Add("cardnum", "12345");
			parameters.Add("emonth", "10");
			parameters.Add("eyear", "2020");
			parameters.Add("cvc", "123");

			string reservationResponseStr = CallApi("reservationOfFixedAmount", parameters);
			ReserveResult paymentResult = _api.ParsePostBackXmlResponse(reservationResponseStr) as ReserveResult;

			Assert.IsNotNull(paymentResult);
			Assert.AreEqual(Result.Success, paymentResult.Result);
		}

		private string CallApi(string method, Dictionary<string,Object> parameters)
		{
			using (WebClient wc = new WebClient())
			{
				wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
				wc.Credentials = new NetworkCredential(GatewayConstants.username, GatewayConstants.password);
				string parameterStr = ParameterHelper.Convert(parameters);
				return wc.UploadString(GatewayConstants.gatewayUrl+method, parameterStr);
			}
		}
	}
}
