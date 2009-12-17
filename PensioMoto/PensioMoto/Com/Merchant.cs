using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;
using PensioMoto.Service.Dto;
using System.Globalization;

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
			_merchantApi.Initialize(gatewayUrl, terminal, apiUsername, apiPassword);
		}

		public IComPaymentResult Capture(string paymentId, double amount)
		{
			return getMockPaymentResult(paymentId, amount);
		}

		public IComPaymentResult Refund(string paymentId, double amount)
		{
			return getMockPaymentResult(paymentId, amount);
		}

		public IComPaymentResult Release(string paymentId)
		{
			return getMockPaymentResult(paymentId, 0);
		}

		public IComPaymentResult Split(string paymentId, double amount)
		{
			return getMockPaymentResult(paymentId, amount);
		}

		public IComPaymentResult GetPayment(string paymentId)
		{
			return getMockPaymentResult(paymentId, 0);
		}

		public IComPaymentResult CaptureRecurring(string recurringPaymentId, double amount)
		{
			return getMockPaymentResult("5", amount);
		}

		public IComPaymentResult PreauthRecurring(string recurringPaymentId, double amount)
		{
			return getMockPaymentResult("5", amount);
		}

		private IComPaymentResult getMockPaymentResult(string paymentId, double amount)
		{
			return new ComPaymentResult(new PaymentResult()
			{
				Result = Result.Success,
				Payment = new Payment
				{
					CapturedAmountString = amount.ToString("0.##", CultureInfo.InvariantCulture),
					ReservedAmountString = amount.ToString("0.##", CultureInfo.InvariantCulture),
					PaymentId = paymentId,
					CreditCardMaskedPan = "444444******4444",
					CreditCardToken = "abcdefghijklmn",
					ShopOrderId = "TST00001",
					Terminal = "Test terminal"
				}
			});
			
		}
	}
}
