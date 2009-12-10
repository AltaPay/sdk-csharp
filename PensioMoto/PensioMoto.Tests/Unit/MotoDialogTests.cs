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
		private MotoDialog _motoDialog;
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

			_view.Verify(v => v.Initialize(_motoDialog, "orderid", 42.42, PaymentType.payment));
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

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithNewCreditCard()
		{
			PaymentResult result = new PaymentResult { Result = Result.Success };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingNewCreditCard("1234", 1, 2010, 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
				.Returns(result);

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			PaymentResult actualResult = _motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.Close());
			_api.Verify(a => a.ReservationOfFixedAmountMOTO("orderid", 42.42, 208, PaymentType.payment, "1234", 1, 2010, 123));
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithExisitingCreditCard()
		{
			PaymentResult result = new PaymentResult { Result = Result.Success };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingExistingCreditCard("token", 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>()))
				.Returns(result);

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			PaymentResult actualResult = _motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.Close());
			_api.Verify(a => a.ReservationOfFixedAmountMOTO("orderid", 42.42, 208, PaymentType.payment, "token", 123));
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForUserToCancel()
		{
			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			Assert.IsNull(_motoDialog.Show());

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.Close());
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithNewCreditCardButItFailsWaitForUserInputToCancel()
		{
			PaymentResult result = new PaymentResult { Result = Result.Failed };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingNewCreditCard("1234", 1, 2010, 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.EnableView("Payment failed"));
			_view.Verify(v => v.Close());
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithExisitingCreditCardButItFailsWaitForUserInputToCancel()
		{
			PaymentResult result = new PaymentResult { Result = Result.Failed };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingExistingCreditCard("token", 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.EnableView("Payment failed"));
			_view.Verify(v => v.Close());
		}

		[Test]
		public void OnPaymentErrorForNewCreditCardShowErrorStatus()
		{
			PaymentResult result = new PaymentResult { Result = Result.Error };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingNewCreditCard("1234", 1, 2010, 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			_motoDialog.Show();

			_view.Verify(v => v.EnableView("Payment error"));
		}

		[Test]
		public void OnPaymentErrorForExistingCreditCardShowErrorStatus()
		{
			PaymentResult result = new PaymentResult { Result = Result.Error };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingExistingCreditCard("token", 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			_motoDialog.Show();

			_view.Verify(v => v.EnableView("Payment error"));
		}

		[Test]
		public void OnPaymentErrorForNewCreditCardShowSystemErrorStatus()
		{
			PaymentResult result = new PaymentResult { Result = Result.SystemError };

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingNewCreditCard("1234", 1, 2010, 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.EnableView("Payment systemerror"));
			_view.Verify(v => v.Close());
		}

		[Test]
		public void OnPaymentErrorForExistingCreditCardShowSystemErrorStatus()
		{
			PaymentResult result = new PaymentResult { Result = Result.SystemError};

			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingExistingCreditCard("token", 123));
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());

			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
				42.42, 208, PaymentType.payment);
			_motoDialog.Show();

			_view.Verify(v => v.EnableView("Payment systemerror"));
		}

	}
}
