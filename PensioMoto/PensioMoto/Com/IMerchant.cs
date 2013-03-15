using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using PensioMoto.Com;

namespace PensioMoto
{
	[ComVisible(true)]
	public interface IMerchant
	{
		void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal);
		IComPaymentResult Capture(string paymentId, double amount);
		IComPaymentResult CaptureWithIdentifier(string paymentId, double amount, string reconciliationIdentifier);
		IComPaymentResult CaptureWithPaymentDetails(string paymentId, double amount, IPaymentDetails paymentDetails);
		IPaymentDetails CreatePaymentDetails();
		IComPaymentResult Refund(string paymentId, double amount);
		IComPaymentResult RefundWithIdentifier(string paymentId, double amount, string reconciliationIdentifier);
		IComPaymentResult ReleaseReservation(string paymentId);
		IComSplitPaymentResult Split(string paymentId, double amount);
		IComPaymentResult GetPayment(string paymentId);
		IComRecurringResult CaptureRecurring(string recurringPaymentId, double amount);
		IComRecurringResult PreauthRecurring(string recurringPaymentId, double amount);
		IComFundingsResult GetFundings(int page);
	}
}
