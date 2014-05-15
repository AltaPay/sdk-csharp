using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using System.Runtime.InteropServices;
using AltaPay.Service.Dto;
using System.Globalization;

namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class Merchant : IMerchant
	{
		private IMerchantApi _merchantApi;

		public Merchant()
		{
			_merchantApi = new MerchantApi();
		}

		public void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal)
		{
			_merchantApi.Initialize(gatewayUrl, apiUsername, apiPassword, terminal);
		}

		public IComPaymentResult Capture(string paymentId, double amount)
		{
			var request = new CaptureRequest() {
				PaymentId = paymentId, 
				Amount = Amount.Get(amount, Currency.XXX)
			};
			return new ComPaymentResult(_merchantApi.Capture(request));
		}

		public IComPaymentResult CaptureWithIdentifier(string paymentId, double amount, string reconciliationIdentifier)
		{
			var request = new CaptureRequest() {
				PaymentId = paymentId, 
				Amount = Amount.Get(amount, Currency.XXX), 
				ReconciliationId = reconciliationIdentifier
			};
			return new ComPaymentResult(_merchantApi.Capture(request));
		}
		
		public IComPaymentResult CaptureWithPaymentDetails(string paymentId, double amount, IPaymentDetails paymentDetails)
		{
			var request = new CaptureRequest() {
				PaymentId = paymentId, 
				Amount = Amount.Get(amount, Currency.XXX), 
				ReconciliationId = paymentDetails.ReconciliationIdentifier,
				InvoiceNumber = paymentDetails.InvoiceNumber,
				SalesTax = paymentDetails.SalesTax,
				OrderLines = ((PaymentDetails)paymentDetails).getLines(),
			};
			return new ComPaymentResult(_merchantApi.Capture(request));
		}		

		public IComPaymentResult Refund(string paymentId, double amount)
		{
			var request = new RefundRequest { 
				PaymentId = paymentId,
				Amount = Amount.Get(amount, Currency.XXX)
			};
			return new ComPaymentResult(_merchantApi.Refund(request));
		}

		public IComPaymentResult RefundWithIdentifier(string paymentId, double amount, string reconciliationIdentifier)
		{
			var request = new RefundRequest { 
				PaymentId = paymentId,
				Amount = Amount.Get(amount, Currency.XXX),
				ReconciliationId = reconciliationIdentifier
			};
			return new ComPaymentResult(_merchantApi.Refund(request));
		}

		public IComPaymentResult ReleaseReservation(string paymentId)
		{
			var request = new ReleaseRequest { 
				PaymentId = paymentId,
			};
			return new ComPaymentResult(_merchantApi.Release(request));
		}
/*
		public IComSplitPaymentResult Split(string paymentId, double amount)
		{
			return new ComSplitPaymentResult(_merchantApi.Split(paymentId, amount));
		}
*/
		public IComPaymentResult GetPayment(string paymentId)
		{
			return new ComPaymentResult(_merchantApi.GetPayment(paymentId));
		}

		public IComRecurringResult CaptureRecurring(string subscriptionId, double amount)
		{
			var request = new ChargeSubscriptionRequest {
				SubscriptionId = subscriptionId,
				Amount = Amount.Get(amount, Currency.XXX)
			};
			return new ComRecurringResult(_merchantApi.ChargeSubscription(request));
		}

		public IComRecurringResult PreauthRecurring(string recurringPaymentId, double amount)
		{
			return new ComRecurringResult(_merchantApi.PreauthRecurring(recurringPaymentId, amount));
		}

		public IComFundingsResult GetFundings(int page)
		{
			return new ComFundingsResult(_merchantApi.getFundings(page));
		}

		public IComPaymentResult CaptureWithOrderlines(string paymentId, double amount, IPaymentDetails paymentDetails)
		{
			var request = new CaptureRequest() {
				PaymentId = paymentId, 
				Amount = Amount.Get(amount, Currency.XXX), 
				ReconciliationId = paymentDetails.ReconciliationIdentifier,
				InvoiceNumber = paymentDetails.InvoiceNumber,
				SalesTax = paymentDetails.SalesTax,
				OrderLines = ((PaymentDetails)paymentDetails).getLines(),
			};
			return new ComPaymentResult(_merchantApi.Capture(request));
		}

		public IPaymentDetails CreatePaymentDetails()
		{
			return new PaymentDetails();
		}
	}
}
