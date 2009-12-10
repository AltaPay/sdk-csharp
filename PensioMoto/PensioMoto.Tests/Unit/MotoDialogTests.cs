using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NUnit.Framework;
using Moq;
using PensioMoto.Service;

namespace PensioMoto.Tests.Unit
{
	[TestFixture]
	public class MotoDialogTests
	{
		private IMotoDialog _motoDialog;
		private Mock<IMotoDialogView> _view;
		private Mock<IMerchantApi> _api;

		[SetUp]
		public void Setup()
		{
			_view = new Mock<IMotoDialogView>();
			_api = new Mock<IMerchantApi>();

			_motoDialog = new MotoDialog(_view.Object, _api.Object);
		}

		[Test]
		public void WhenCallingInitializeOnMotoDialogCallInitializeOnView()
		{
			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid", 
				42.42, 208, PaymentType.payment);

			_view.Verify(v => v.Initialize("orderid", 42.42, PaymentType.payment));
		}

		[Test]
		public void WhenCallingInitializeOnMotoDialogCallInitializeOnApi()
		{
			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);

			_api.Verify(a => a.Initialize("gatewayurl", "terminal", "apiusername", "apipassword"));
		}

		[Test]
		public void WhenCallingSetCreditCardOnMotoDialogCallSetCreditCardOnView()
		{
			_motoDialog.SetCreditCard("maskedpan", "cardtoken");

			_view.Verify(v => v.SetCreditCard("maskedpan", "cardtoken"));
		}
	}
}
