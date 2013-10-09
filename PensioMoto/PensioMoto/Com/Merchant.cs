using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;
using PensioMoto.Service.Dto;
using System.Globalization;
using PensioMoto;

namespace PensioMoto
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
			return new ComPaymentResult(_merchantApi.Capture(paymentId, amount));
		}

		public IComPaymentResult CaptureWithIdentifier(string paymentId, double amount, string reconciliationIdentifier)
		{
			return new ComPaymentResult(_merchantApi.Capture(paymentId, amount, reconciliationIdentifier));
		}
		
		public IComPaymentResult CaptureWithPaymentDetails(string paymentId, double amount, IPaymentDetails paymentDetails)
		{
			return new ComPaymentResult(_merchantApi.Capture(paymentId, amount, (PaymentDetails)paymentDetails));
		}		

		public IComPaymentResult Refund(string paymentId, double amount)
		{
			return new ComPaymentResult(_merchantApi.Refund(paymentId, amount));
		}

		public IComPaymentResult RefundWithIdentifier(string paymentId, double amount, string reconciliationIdentifier)
		{
			return new ComPaymentResult(_merchantApi.Refund(paymentId, amount, reconciliationIdentifier));
		}

		public IComPaymentResult ReleaseReservation(string paymentId)
		{
			return new ComPaymentResult(_merchantApi.Release(paymentId));
		}

		public IComSplitPaymentResult Split(string paymentId, double amount)
		{
			return new ComSplitPaymentResult(_merchantApi.Split(paymentId, amount));
		}

		public IComPaymentResult GetPayment(string paymentId)
		{
			return new ComPaymentResult(_merchantApi.GetPayment(paymentId));
		}

		public IComRecurringResult CaptureRecurring(string recurringPaymentId, double amount)
		{
			return new ComRecurringResult(_merchantApi.CaptureRecurring(recurringPaymentId, amount));
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
			return new ComPaymentResult(_merchantApi.Capture(paymentId, amount, (PaymentDetails)paymentDetails));
		}

		public IPaymentDetails CreatePaymentDetails()
		{
			return new PaymentDetails();
		}
	}
}
