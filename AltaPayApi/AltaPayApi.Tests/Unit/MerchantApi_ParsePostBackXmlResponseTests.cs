using System;
using NUnit.Framework;
using AltaPay.Api.Tests;
using AltaPay.Service.Loggers;

namespace AltaPay.Service.Tests.Unit
{
	public class MerchantApi_ParsePostBackXmlResponseTests : BaseTest
	{
		[Test]
		public void ParsePostBackXmlResponse_Success()
		{
			string xmlResponse = @"<?xml version=""1.0""?>
<APIResponse version=""20130430""><Header><Date>2014-11-07T14:30:48+01:00</Date><Path>/</Path><ErrorCode>0</ErrorCode><ErrorMessage></ErrorMessage></Header><Body><Result>Error</Result><MerchantErrorMessage>Invalid account number (no such number)[54321]</MerchantErrorMessage><CardHolderErrorMessage>Internal Error</CardHolderErrorMessage><Transactions><Transaction><TransactionId>391</TransactionId><AuthType>payment</AuthType><CardStatus>InvalidLuhn</CardStatus><CreditCardExpiry><Year>2016</Year><Month>10</Month></CreditCardExpiry><CreditCardToken>fe36d10bdb1ab8c1139605b18e8acece2755b254</CreditCardToken><CreditCardMaskedPan>457160******1575</CreditCardMaskedPan><ThreeDSecureResult>Not_Applicable</ThreeDSecureResult><CVVCheckResult>Not_Applicable</CVVCheckResult><BlacklistToken>50c5ec1e29b71aae26a8c309860323dbfbbf97f4</BlacklistToken><ShopOrderId>1418112</ShopOrderId><Shop>Freetrailer</Shop><Terminal>Freetrailer CC DKK</Terminal><TransactionStatus>preauth_error</TransactionStatus><MerchantCurrency>208</MerchantCurrency><CardHolderCurrency>208</CardHolderCurrency><ReservedAmount>0.00</ReservedAmount><CapturedAmount>0.00</CapturedAmount><RefundedAmount>0.00</RefundedAmount><RecurringDefaultAmount>0.00</RecurringDefaultAmount><CreatedDate>2014-11-07 14:30:45</CreatedDate><UpdatedDate>2014-11-07 14:30:47</UpdatedDate><PaymentNature>CreditCard</PaymentNature><PaymentSchemeName>Visa</PaymentSchemeName><PaymentNatureService name=""ValitorAcquirer""><SupportsRefunds>true</SupportsRefunds><SupportsRelease>true</SupportsRelease><SupportsMultipleCaptures>true</SupportsMultipleCaptures><SupportsMultipleRefunds>true</SupportsMultipleRefunds></PaymentNatureService><ChargebackEvents/><PaymentInfos/><CustomerInfo><UserAgent>Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.111 Safari/537.36</UserAgent><IpAddress>86.58.170.35</IpAddress><Email><![CDATA[fk@freetrailer.dk]]></Email><Username/><CustomerPhone>28911648</CustomerPhone><OrganisationNumber></OrganisationNumber><CountryOfOrigin><Country>DK</Country><Source>CardNumber</Source></CountryOfOrigin><BillingAddress><Firstname><![CDATA[Fie]]></Firstname><Lastname/><Address><![CDATA[sdf]]></Address><City><![CDATA[sdf]]></City><Region/><Country><![CDATA[De]]></Country><PostalCode><![CDATA[2400]]></PostalCode></BillingAddress></CustomerInfo><ReconciliationIdentifiers/></Transaction></Transactions></Body></APIResponse>";
			
			var merchantApi = new MerchantApi("url", "username", "password");
			merchantApi.ParsePostBackXmlResponse(xmlResponse);
			/*
			Assert.AreEqual(false, result.HasAnyFailedPaymentActions());
			Assert.AreEqual(2, result.PaymentActions.Count);
			
			Assert.AreEqual(Result.Success, result.PaymentActions[0].Result);
			Assert.AreEqual(12.34m, result.PaymentActions[0].Payment.ReservedAmount);
			
			Assert.AreEqual(Result.Success, result.PaymentActions[1].Result);
			Assert.AreEqual(98.76m, result.PaymentActions[1].Payment.ReservedAmount);
			*/
		}
	
		
		[Test]
		[ExpectedException(typeof(Exception))]
		public void ParsePostBackXmlResponse_InvalidXml()
		{
			var logger = new FileAltaPayLogger("/tmp/skarptests");
			logger.LogLevel = AltaPayLogLevel.Error;
			
			string xmlResponse = @"<?xml version=""1.0""?><APIResponse version=""20130430""><NotValid>Not even a little bit</NotValid></APIResponse>";
			
			var merchantApi = new MerchantApi("url", "username", "password", logger);
			merchantApi.ParsePostBackXmlResponse(xmlResponse);
		}
	}
}