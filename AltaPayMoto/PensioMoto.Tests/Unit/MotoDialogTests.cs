using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using NUnit.Framework;
using Moq;
using AltaPay.Service;
using AltaPay.Moto;

namespace AltaPay.Moto.Tests.Unit
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

			_api.Verify(a => a.Initialize("gatewayurl", "apiusername", "apipassword", "terminal"));
		}

		[Test]
		public void WhenCallingSetCreditCardOnMotoDialogCallSetCreditCardOnView()
		{
			_motoDialog.AddCreditCard("maskedpan", "cardtoken");

			_view.Verify(v => v.AddCreditCard("maskedpan", "cardtoken"));
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithNewCreditCard()
		{
			SetupResults(new PaymentResult { Result = Result.Success }, false);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.Close());
			_api.Verify(a => a.ReservationOfFixedAmountMOTO("orderid", 42.42, 208, PaymentType.payment, "1234", 1, 2010, "123", null));
		}

		

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithExisitingCreditCard()
		{
			SetupResults(new PaymentResult { Result = Result.Success }, true);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.Close());
			_api.Verify(a => a.ReservationOfFixedAmountMOTO("orderid", 42.42, 208, PaymentType.payment, "token", "123", null));
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForUserToCancel()
		{
			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.Cancel());


			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.Close());
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithNewCreditCardButItFailsWaitForUserInputToCancel()
		{
			SetupResults(new PaymentResult { Result = Result.Failed, ResultMerchantMessage = "merchant error message" }, false);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.EnableView("Payment failed: merchant error message"));
			_view.Verify(v => v.Close());
		}

		[Test]
		public void WhenCallingShowOnMotoDialogCallShowOnViewAndWaitForToPayWithExisitingCreditCardButItFailsWaitForUserInputToCancel()
		{
			SetupResults(new PaymentResult { Result = Result.Failed, ResultMerchantMessage = "merchant error message" }, true);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.EnableView("Payment failed: merchant error message"));
			_view.Verify(v => v.Close());
		}

		[Test]
		public void OnPaymentErrorForNewCreditCardShowErrorStatus()
		{
			SetupResults(new PaymentResult { Result = Result.Error, ResultMerchantMessage = "merchant error message" }, false);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.EnableView("Payment error: merchant error message"));
		}

		[Test]
		public void OnPaymentErrorForExistingCreditCardShowErrorStatus()
		{
			SetupResults(new PaymentResult { Result = Result.Error, ResultMerchantMessage = "merchant error message" }, true);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.EnableView("Payment error: merchant error message"));
		}

		[Test]
		public void OnPaymentErrorForNewCreditCardShowSystemErrorStatus()
		{
			SetupResults(new PaymentResult { Result = Result.SystemError, ResultMerchantMessage = "merchant error message" }, false);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.ShowBlocking());
			_view.Verify(v => v.EnableView("Payment systemerror: merchant error message"));
			_view.Verify(v => v.Close());
		}

		[Test]
		public void OnPaymentErrorForExistingCreditCardShowSystemErrorStatus()
		{
			SetupResults(new PaymentResult { Result = Result.SystemError, ResultMerchantMessage = "merchant error message" }, true);

			InitializePayment();
			_motoDialog.Show();

			_view.Verify(v => v.EnableView("Payment systemerror: merchant error message"));
		}

		[Test]
		public void OnPaymentSuccessReturnPaymentResult()
		{
			PaymentResult expected = new PaymentResult { Result = Result.Success };
			SetupResults(expected, true);

			InitializePayment();
			PaymentResult actual = _motoDialog.Show();

			Assert.AreSame(expected, actual);
		}

		[Test]
		public void OnUserCancelReturnPaymentResultWithUserAbort()
		{
			_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.Cancel());

			InitializePayment();
			PaymentResult actual = _motoDialog.Show();

			Assert.AreEqual(Result.AbortedByUser, actual.Result);
		}

		[Test]
		public void OnUserCancelWhenAnErrorResultHaveBeenMadeReturnPaymentResultFromApi()
		{
			PaymentResult expected = new PaymentResult { Result = Result.Error };
			SetupResults(expected, true);

			InitializePayment();
			PaymentResult actual = _motoDialog.Show();

			Assert.AreSame(expected, actual);
		}

		[Test]
		public void OnUserCancelWhenAnFailedResultHaveBeenMadeReturnPaymentResultFromApi()
		{
			PaymentResult expected = new PaymentResult { Result = Result.Failed };
			SetupResults(expected, true);

			InitializePayment();
			PaymentResult actual = _motoDialog.Show();

			Assert.AreSame(expected, actual);
		}

		[Test]
		public void OnUserCancelWhenASystemErrorResultHaveBeenMadeReturnPaymentResultFromApi()
		{
			PaymentResult expected = new PaymentResult { Result = Result.SystemError };
			SetupResults(expected, true);

			InitializePayment();
			PaymentResult actual = _motoDialog.Show();

			Assert.AreSame(expected, actual);
		}

	
		private void SetupResults(PaymentResult result, bool setupExisting)
		{
			if (!setupExisting)
			{
				_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingNewCreditCard("1234", 1, 2010, "123", null));
			}
			else
			{
				_view.Setup(v => v.ShowBlocking()).Callback(() => _motoDialog.PayUsingExistingCreditCard("token", "123", null));
			}
			
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<AvsInfo>()))
				.Returns(result);
			_api.Setup(a => a.ReservationOfFixedAmountMOTO(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<PaymentType>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AvsInfo>()))
				.Returns(result);
			_view.Setup(v => v.EnableView(It.IsAny<string>())).Callback(() => _motoDialog.Cancel());
		}

		private void InitializePayment()
		{
			_motoDialog.Initialize("gatewayurl", "apiusername", "apipassword", "terminal", "orderid",
						 42.42, 208, PaymentType.payment);
		}
	}
}
