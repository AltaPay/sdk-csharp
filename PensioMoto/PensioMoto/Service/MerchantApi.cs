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
		private string _gatewayUrl;
		private string _terminal;
		private string _username;
		private string _password;

		public void Initialize(string gatewayUrl, string terminal, string username, string password)
		{
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
			string url = _gatewayUrl + "reservationOfFixedAmountMOTO" +
				"?terminal=" + _terminal +
				"&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&cardnum=" + pan +
				"&emonth=" + expiryMonth.ToString() +
				"&eyear=" + expiryYear.ToString() +
				"&cvc=" + cvc;

			return GetResultFromUrl(url);
		}

		public PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId, 
            double amount, 
            int currency, 
            PaymentType paymentType, 
            string creditCardToken, 
            string cvc)
		{
			string url = _gatewayUrl + "reservationOfFixedAmountMOTO" +
				"?terminal=" + _terminal +
				"&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&credit_card_token=" + creditCardToken +
				"&cvc=" + cvc;

			return GetResultFromUrl(url);
		}

		private PaymentResult GetResultFromUrl(string url)
		{
			WebRequest request = WebRequest.Create(url);
			request.Credentials = new NetworkCredential(_username, _password);
			WebResponse response = request.GetResponse();

			XmlSerializer serializer = new XmlSerializer(typeof(ApiResponse));
			ApiResponse apiResponse = (ApiResponse)serializer.Deserialize(response.GetResponseStream());

			return GetResultFromXml(apiResponse);
		}

		private PaymentResult GetResultFromXml(ApiResponse apiResponse)
		{
			return new PaymentResult
			{
				Result = (apiResponse.Header.ErrorCode == 0 ?(Result)Enum.Parse(typeof(Result), apiResponse.Body.Result) : Result.SystemError),
				ResultMessage = (apiResponse.Header.ErrorCode == 0 ? apiResponse.Body.CardHolderErrorMessage : apiResponse.Header.ErrorMessage),
				Payment = (apiResponse.Body.Transactions != null ? GetPayment(apiResponse.Body.Transactions[0]) : null)
			};
		}

		private Payment GetPayment(Transaction transaction)
		{
			return new Payment
			{
				PaymentId = transaction.TransactionId,
				ShopOrderId = transaction.ShopOrderId,
				Terminal = transaction.Terminal,
				ReservedAmount = transaction.ReservedAmount,
				CapturedAmount = transaction.CapturedAmount,
				PaymentStatus = transaction.TransactionStatus,
				CreditCardToken = transaction.CreditCardToken,
				CreditCardMaskedPan = transaction.CreditCardMaskedPan
			};
		}
	}
}
