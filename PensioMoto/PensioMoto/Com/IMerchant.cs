using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	public interface IMerchant
	{
		void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal);
		IComPaymentResult Capture(string paymentId, double amount);
		IComPaymentResult Refund(string paymentId, double amount);
		IComPaymentResult Release(string paymentId);
		IComPaymentResult Split(string paymentId, double amount);
		IComPaymentResult GetPayment(string paymentId);
		IComPaymentResult CaptureRecurring(string recurringPaymentId, double amount);
		IComPaymentResult PreauthRecurring(string recurringPaymentId, double amount);
	}
}
