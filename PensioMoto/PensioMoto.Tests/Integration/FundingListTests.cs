﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using NUnit.Framework;

namespace PensioMoto.Tests.Integration
{
	[TestFixture]
	class FundingListTests
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi();
			//_api.Initialize("https://ci.gateway.pensio.com/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal");
			_api.Initialize("http://10.101.97.14/merchant.php/API/", "shop api", "testpassword", "Pensio Test Terminal");
		}

		[Test]
		public void CallingMerchantApiWithSuccessfulParametersReturnsSuccessfulResult()
		{
			FundingsResult result = _api.getFundings(0);

			Assert.AreEqual(Result.Success, result.Result);
			Assert.AreEqual(1, result.Pages);
			Assert.AreEqual(1, result.Fundings);
		}
	}
}
