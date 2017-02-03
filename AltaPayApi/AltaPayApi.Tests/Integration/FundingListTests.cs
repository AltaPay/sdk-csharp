﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using NUnit.Framework;
using AltaPay.Api.Tests;
using AltaPay.Service.Dto;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	class FundingListTests : BaseTest
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
			//_api = new MerchantApi("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword");
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult()
		{
			FundingsResult result = _api.GetFundings(new GetFundingsRequest { Page = 0 });

			Assert.AreEqual(Result.Success, result.Result);
			Assert.AreEqual(1, result.Pages);
			Assert.IsTrue(result.Fundings.Count >= 1);
		}

		[Test]
		public void FundingDownloadTest()
		{
			FundingsResult result = _api.GetFundings(new GetFundingsRequest { Page = 0 });

			Assert.AreEqual(Result.Success, result.Result);
			Assert.AreEqual(1, result.Pages);
			Assert.IsTrue(result.Fundings.Count >= 1);

			Funding funding = result.Fundings[0];

			FundingContentResult fres = _api.GetFundingContent(funding);

			FundingRecord record = fres.GetFundingRecordList().ElementAt(0);

			Assert.AreEqual(new DateTime(2010, 12, 24), record.FundingDate);
			Assert.AreEqual("fee", record.RecordType);
			Assert.AreEqual("Monthly fee", record.Id);
			Assert.AreEqual("", record.ReconciliationIdentifier);
			Assert.AreEqual("", record.PaymentId);
			Assert.AreEqual("", record.OrderId);
			Assert.AreEqual("", record.Terminal);
			Assert.AreEqual("AltaPay Functional Test Shop", record.Shop);

			Amount amount = Amount.Get(0.0M, Currency.EUR);

			Assert.AreEqual(amount, record.PaymentAmount);
			Assert.AreEqual(1.0M, record.ExchangeRate);
			Assert.AreEqual(amount, record.FundingAmount);
			Assert.AreEqual(amount, record.FixedFeeAmount);
			Assert.AreEqual(amount, record.FixedFeeVatAmount);
			Assert.AreEqual(amount, record.RateBasedFeeAmount);
			Assert.AreEqual(amount, record.RateBasedFeeVatAmount);


		}
	}
}
