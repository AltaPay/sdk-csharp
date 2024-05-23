using NUnit.Framework;
using AltaPay.Api.Tests;

namespace AltaPay.Service.Tests.Integration
{
	[TestFixture]
	class CalculateSurcharge : BaseTest
	{
		IMerchantApi _api;

		[SetUp]
		public void Setup ()
		{
			_api = new MerchantApi (GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
		}

		[Test]
		public void CallingMerchantApiReturnsSuccessfulResult ()
		{
			CalculateSurchargeRequest calculateSurcharge = new CalculateSurchargeRequest ()
			{
				Amount = Amount.Get (120.20, Currency.EUR),
				PaymentId = "52524771"
			};

			CalculateSurchargeResult result = _api.CalculateSurcharge(calculateSurcharge);

			Assert.AreEqual (Result.Success, result.Result);
		}
	}
}
