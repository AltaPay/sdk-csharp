using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using NUnit.Framework;

namespace AltaPay.Moto.Tests.Integration
{
	[TestFixture]
	class FundingListTests
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi();
			//_api.Initialize("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword", "AltaPay Soap Test Terminal");
			_api.Initialize("http://gateway.dev.pensio.com/merchant.php/API/", "shop api", "testpassword", "AltaPay Soap Test Terminal");
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult()
		{
			FundingsResult result = _api.GetFundings( new GetFundingsRequest { Page = 0});

			Assert.AreEqual(Result.Success, result.Result);
			Assert.AreEqual(1, result.Pages);
			Assert.AreEqual(1, result.Fundings.Count);
		}
	}
}
