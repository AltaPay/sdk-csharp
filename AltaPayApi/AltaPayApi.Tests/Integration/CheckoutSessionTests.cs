using NUnit.Framework;
using AltaPay.Service;
using System;

namespace AltaPay.Service.Tests.Integration
{
    [TestFixture]
    public class CheckoutSessionTests : AltaPay.Api.Tests.BaseTest
    {
        IMerchantApi _api;

        [SetUp]
        public void Setup()
        {
            _api = new MerchantApi(GatewayConstants.gatewayUrl, GatewayConstants.username, GatewayConstants.password);
        }

        [Test]
        public void CreateCheckoutSession_Success()
        {
            var request = new CheckoutSessionRequest
            {
                ShopOrderId = "checkout-test-" + Guid.NewGuid().ToString(),
                Amount = Amount.Get(10.50, Currency.EUR),
                Terminal = GatewayConstants.terminal,
                SessionId = "session-" + Guid.NewGuid().ToString()
            };
            
            request.Terminals.Add(GatewayConstants.terminal);

            var result = _api.CheckoutSession(request);

            Assert.AreEqual(Result.Success, result.Result);
            Assert.IsNotNull(result.SessionId);
            Assert.IsNotEmpty(result.SessionId);
            Assert.AreEqual("CREATED", result.SessionStatus);
        }

        [Test]
        public void CreateCheckoutSession_FailsOnDuplicateSessionId()
        {
            var sessionId = "session-" + Guid.NewGuid().ToString();

            var request = new CheckoutSessionRequest
            {
                ShopOrderId = "checkout-test-" + Guid.NewGuid().ToString(),
                Amount = Amount.Get(10.50, Currency.EUR),
                Terminal = GatewayConstants.terminal,
                SessionId = sessionId
            };
            request.Terminals.Add(GatewayConstants.terminal);

            // First call succeeds
            var firstResult = _api.CheckoutSession(request);
            Assert.AreEqual(Result.Success, firstResult.Result);

            // Second call with same SessionId fails
            var duplicateResult = _api.CheckoutSession(request);
            Assert.AreEqual(Result.Error, duplicateResult.Result);
            Assert.IsNotNull(duplicateResult.ResultMerchantMessage);
            Assert.IsTrue(duplicateResult.ResultMerchantMessage.Contains("already exists"));
        }
    }
}
