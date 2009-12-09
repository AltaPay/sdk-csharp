using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;
using System.Globalization;

namespace PensioMoto.Service
{
	public class MerchantApi : IMerchantApi
	{
		private string _gatewayUrl;
		private string _terminal;
		private string _username;
		private string _password;

		public MerchantApi(string gatewayUrl, string terminal, string username, string password)
		{
			_gatewayUrl = gatewayUrl;
			_terminal = terminal;
			_username = username;
			_password = password;
		}

		public PaymentResult ReservationOfFixedAmountMOTO(string shopOrderId, 
			double amount, 
			int currency, 
			PaymentType paymentType, 
			string pan, 
			int expiryMonth, 
			int expiryYear, 
			int cvc)
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
				"&cvc=" + cvc.ToString();

			return GetResultFromUrl(url);
		}

		public PaymentResult ReservationOfFixedAmountMOTO(string shopOrderId, double amount, int currency, PaymentType paymentType, string creditCardToken, int cvc)
		{
			string url = _gatewayUrl + "reservationOfFixedAmountMOTO" +
				"?terminal=" + _terminal +
				"&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&credit_card_token=" + creditCardToken +
				"&cvc=" + cvc.ToString();
			//throw new Exception(url);
			return GetResultFromUrl(url);
		}

		private PaymentResult GetResultFromUrl(string url)
		{
			WebRequest request = WebRequest.Create(url);
			request.Credentials = new NetworkCredential(_username, _password);
			WebResponse response = request.GetResponse();

			XPathDocument xpathDoc = new XPathDocument(response.GetResponseStream());
			return GetResultFromXml(xpathDoc);
		}

		private PaymentResult GetResultFromXml(XPathDocument xpathDoc)
		{
			XPathNavigator navigator = xpathDoc.CreateNavigator();

			PaymentResult result = new PaymentResult();
			if (ResponseHasSystemError(navigator))
			{
				result.Result = Result.SystemError;
				result.ResultMessage = navigator.SelectSingleNode("/APIResponse/Header/ErrorMessage").ToString();
			}
			else
			{
				result.Result = (Result)Enum.Parse(typeof(Result), navigator.SelectSingleNode("/APIResponse/Body/Result").ToString());
				if (result.Result != Result.Success)
				{
					result.ResultMessage = navigator.SelectSingleNode("/APIResponse/Body/CardHolderErrorMessage").ToString();
				}
			}
			SetPaymentOnResultIfExists(navigator.SelectSingleNode("/APIResponse/Body/Transactions/Transaction"), result);
			
			return result;
		}

		private static bool ResponseHasSystemError(XPathNavigator navigator)
		{
			return navigator.SelectSingleNode("/APIResponse/Header/ErrorCode").ToString() != "0";
		}

		private static void SetPaymentOnResultIfExists(XPathNavigator navigator, PaymentResult result)
		{
			if (navigator != null)
			{
				result.Payment = new Payment();
				result.Payment.PaymentId = int.Parse(navigator.SelectSingleNode("TransactionId").ToString());
				result.Payment.ShopOrderId = navigator.SelectSingleNode("ShopOrderId").ToString();
				result.Payment.Terminal = navigator.SelectSingleNode("Terminal").ToString();
				if (navigator.SelectSingleNode("ReservedAmount").ToString().Length > 0)
					result.Payment.ReservedAmount = double.Parse(navigator.SelectSingleNode("ReservedAmount").ToString(), CultureInfo.InvariantCulture);
				if (navigator.SelectSingleNode("CapturedAmount").ToString().Length > 0)
					result.Payment.CapturedAmount = double.Parse(navigator.SelectSingleNode("CapturedAmount").ToString(), CultureInfo.InvariantCulture);
				result.Payment.PaymentStatus = navigator.SelectSingleNode("TransactionStatus").ToString();
				result.Payment.CreditCardToken = navigator.SelectSingleNode("CreditCardToken").ToString();
				result.Payment.CreditCardMaskedPan = navigator.SelectSingleNode("CreditCardMaskedPan").ToString();
			}
		}

		
	}
}
