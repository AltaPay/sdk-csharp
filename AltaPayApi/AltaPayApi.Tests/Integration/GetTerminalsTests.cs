using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using NUnit.Framework;
using AltaPay.Api.Tests;
using AltaPay.Service.Dto;
using System.IO;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	class GetTerminalsTests : BaseTest
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup()
		{
			_api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void CallingMerchantApiReturnsSuccessfulResult()
		{
			GetTerminalsResult result = _api.GetTerminals();

			Assert.AreEqual(Result.Success, result.Result);
        }
	}
}
