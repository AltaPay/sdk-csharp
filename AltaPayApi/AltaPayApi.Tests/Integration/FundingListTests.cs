using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using NUnit.Framework;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	class FundingListTests : BaseTest
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi("http://gateway.dev.pensio.com/merchant.php/API/", "shop api", "testpassword");
			//_api = new MerchantApi("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword");
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult()
		{
			FundingsResult result = _api.GetFundings( new GetFundingsRequest { Page = 0});

			Assert.AreEqual(Result.Success, result.Result);
			Assert.AreEqual(1, result.Pages);
			Assert.IsTrue(result.Fundings.Count>=1);
		}
	}
}
