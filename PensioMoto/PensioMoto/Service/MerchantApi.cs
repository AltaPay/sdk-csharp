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

		public void Initialize(string gatewayUrl, string terminal, string username, string password)
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

			ApiResponse apiResponse = (ApiResponse)_apiResponseDeserializer.Deserialize(response.GetResponseStream());

			return GetResultFromXml(apiResponse);
		}

		private PaymentResult GetResultFromXml(ApiResponse apiResponse)
		{
			if(apiResponse.Header.ErrorCode == 0)
			{
				return new PaymentResult
				{
					Result = (Result)Enum.Parse(typeof(Result), apiResponse.Body.Result),
					ResultMessage = apiResponse.Body.CardHolderErrorMessage,
					Payment = (apiResponse.Body.Transactions != null ? apiResponse.Body.Transactions[0] : null)
				};
			}
			else
			{
				return new PaymentResult
				{
					Result = Result.SystemError,
					ResultMessage = apiResponse.Header.ErrorMessage
				};
			}
		}
	}
}
