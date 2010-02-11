using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Xml.XPath;
using PensioMoto.Service.Dto;

namespace PensioMoto.Service
{
	public class MerchantApi : IMerchantApi
	{
		private XmlSerializer _apiResponseDeserializer;
		private string _gatewayUrl;
		private string _terminal;
		private string _username;
		private string _password;

		public void Initialize(string gatewayUrl, string username, string password, string terminal)
		{
			_apiResponseDeserializer = new XmlSerializer(typeof(ApiResponse));

			_gatewayUrl = gatewayUrl;
			_terminal = terminal;
			_username = username;
			_password = password;
		}

		public PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId, 
			double amount, 
			int currency, 
			PaymentType paymentType, 
			string pan, 
			int expiryMonth, 
			int expiryYear, 
			string cvc)
		{
			string parameters = "&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&cardnum=" + pan +
				"&emonth=" + expiryMonth.ToString() +
				"&eyear=" + expiryYear.ToString() +
				"&cvc=" + cvc;

			return new PaymentResult(GetResultFromUrl("reservationOfFixedAmountMOTO", parameters));
		}

		public PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId, 
            double amount, 
            int currency, 
            PaymentType paymentType, 
            string creditCardToken, 
            string cvc)
		{
			string parameters = "&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&credit_card_token=" + creditCardToken +
				"&cvc=" + cvc;

			return new PaymentResult(GetResultFromUrl("reservationOfFixedAmountMOTO", parameters));
		}

		public PaymentResult Capture(string paymentId, double amount)
		{
			return new PaymentResult(GetResultFromUrl("captureReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public PaymentResult Refund(string paymentId, double amount)
		{
			return new PaymentResult(GetResultFromUrl("refundCapturedReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public PaymentResult Release(string paymentId)
		{
			return new PaymentResult(GetResultFromUrl("releaseReservation",
				"&transaction_id=" + paymentId));
		}

		public SplitPaymentResult Split(string paymentId, double amount)
		{
			return new SplitPaymentResult(GetResultFromUrl("splitTransaction",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public PaymentResult GetPayment(string paymentId)
		{
			return new PaymentResult(GetResultFromUrl("transactions",
				"&transaction=" + paymentId));
		}

		public RecurringResult CaptureRecurring(string recurringPaymentId, double amount)
		{
			return new RecurringResult(GetResultFromUrl("captureRecurring",
				"&transaction_id=" + recurringPaymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public RecurringResult PreauthRecurring(string recurringPaymentId, double amount)
		{
			return new RecurringResult(GetResultFromUrl("preauthRecurring",
				"&transaction_id=" + recurringPaymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		private ApiResponse GetResultFromUrl(string method, string parameters)
		{
			try
			{
				string url = _gatewayUrl + method +
						"?terminal=" + _terminal +
						parameters;
				WebRequest request = WebRequest.Create(url);
				request.Credentials = new NetworkCredential(_username, _password);
				WebResponse response = request.GetResponse();

				ApiResponse apiResponse = (ApiResponse)_apiResponseDeserializer.Deserialize(response.GetResponseStream());
				return apiResponse;
			}
			catch (Exception exception)
			{
				ApiResponse response = new ApiResponse();
				response.Header = new Header();
				response.Header.ErrorMessage = exception.Message;
				response.Header.ErrorCode = 1;
				return response;
			}
		}

	}
}
