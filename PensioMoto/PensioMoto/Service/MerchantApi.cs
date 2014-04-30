﻿using System;
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

		public void Initialize(string gatewayUrl, string username, string password, string terminal)
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
			string cvc,
			AvsInfo avsInfo)
		{
			string parameters = "&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&cardnum=" + pan +
				"&emonth=" + expiryMonth.ToString() +
				"&eyear=" + expiryYear.ToString() +
				"&cvc=" + cvc +
				getAvsInfoParameters(avsInfo);

			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("reservationOfFixedAmountMOTO", parameters));
		}

		private string getAvsInfoParameters(AvsInfo avsInfo)
		{
			if (avsInfo != null)
			{
				return "&billing_firstname=" + avsInfo.FirstName
					+ "&billing_lastname=" + avsInfo.LastName
					+ "&billing_address=" + avsInfo.Address
					+ "&billing_city=" + avsInfo.City
					+ "&billing_country=" + avsInfo.Country
					+ "&billing_region=" + avsInfo.Region
					+ "&billing_postal=" + avsInfo.PostalCode
					+ "&email=" + avsInfo.Email
					+ "&customer_phone=" + avsInfo.Phone;
			}
			else
				return "";
		}
		
		private string getPaymentDetailsParameters(PaymentDetails paymentDetails)
		{
			string parameters = "";
			int lineNumber = 0;
			foreach (PaymentOrderLine orderLine in paymentDetails.getLines())
			{
				parameters += "&orderLines["+lineNumber+"][itemId]=" + orderLine.ItemId;
				parameters += "&orderLines["+lineNumber+"][quantity]=" + orderLine.Quantity.ToString("0.##", CultureInfo.InvariantCulture);
				parameters += "&orderLines["+lineNumber+"][taxPercent]=" + orderLine.TaxPercent.ToString("0.##", CultureInfo.InvariantCulture);
				parameters += "&orderLines["+lineNumber+"][unitCode]=" + orderLine.UnitCode;
				parameters += "&orderLines["+lineNumber+"][unitPrice]=" + orderLine.UnitPrice.ToString("0.##", CultureInfo.InvariantCulture);
				parameters += "&orderLines["+lineNumber+"][description]=" + orderLine.Description;
				parameters += "&orderLines["+lineNumber+"][discount]=" + orderLine.Discount.ToString("0.##", CultureInfo.InvariantCulture);
				parameters += "&orderLines["+lineNumber+"][goodsType]=" + orderLine.GoodsType;
				
				lineNumber++;
			}

			if (paymentDetails.ReconciliationIdentifier != null)
			{
				parameters += "&reconciliation_identifier=" + paymentDetails.ReconciliationIdentifier;
			}

			if (paymentDetails.InvoiceNumber != null)
			{
				parameters += "&invoice_number=" + paymentDetails.InvoiceNumber;
			}

			if (paymentDetails.SalesTax != 0)
			{
				parameters += "&sales_tax=" + paymentDetails.SalesTax.ToString("0.##", CultureInfo.InvariantCulture);
			}

			return parameters;
		}

		public PaymentResult ReservationOfFixedAmountMOTO(
            string shopOrderId, 
            double amount, 
            int currency, 
            PaymentType paymentType, 
            string creditCardToken,
			string cvc,
			AvsInfo avsInfo)
		{
			string parameters = "&shop_orderid=" + shopOrderId +
				"&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) +
				"&currency=" + currency.ToString() +
				"&type=" + paymentType.ToString() +
				"&credit_card_token=" + creditCardToken +
				"&cvc=" + cvc +
				getAvsInfoParameters(avsInfo);

			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("reservationOfFixedAmountMOTO", parameters));
		}

		public PaymentResult Capture(string paymentId, double amount, string reconciliationIdentifier)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("captureReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) + "&reconciliation_identifier="+reconciliationIdentifier));
		}

		public PaymentResult Capture(string paymentId, double amount)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("captureReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}
		
		public PaymentResult Capture(string paymentId, double amount, PaymentDetails paymentDetails)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("captureReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) + getPaymentDetailsParameters(paymentDetails)));
		}

		public PaymentResult Refund(string paymentId, double amount, string reconciliationIdentifier)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("refundCapturedReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture) + "&reconciliation_identifier=" + reconciliationIdentifier));
		}

		public PaymentResult Refund(string paymentId, double amount)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("refundCapturedReservation",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public PaymentResult Release(string paymentId)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("releaseReservation",
				"&transaction_id=" + paymentId));
		}

		public SplitPaymentResult Split(string paymentId, double amount)
		{
			return new SplitPaymentResult(GetResultFromUrl<PaymentApiResponse>("splitTransaction",
				"&transaction_id=" + paymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public PaymentResult GetPayment(string paymentId)
		{
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("transactions",
				"&transaction=" + paymentId));
		}

		public RecurringResult CaptureRecurring(string recurringPaymentId, double amount)
		{
			return new RecurringResult(GetResultFromUrl<PaymentApiResponse>("captureRecurring",
				"&transaction_id=" + recurringPaymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}

		public RecurringResult PreauthRecurring(string recurringPaymentId, double amount)
		{
			return new RecurringResult(GetResultFromUrl<PaymentApiResponse>("preauthRecurring",
				"&transaction_id=" + recurringPaymentId + "&amount=" + amount.ToString("0.##", CultureInfo.InvariantCulture)));
		}
		
		public FundingsResult getFundings(int page)
		{
			return new FundingsResult(GetResultFromUrl<FundingsApiResponse>("fundingList",
				"&page=" + page), new NetworkCredential(_username, _password));
		}
		
		public PaymentRequestResult CreatePaymentRequest(PaymentRequest request)
		{
			string parameters = "&shop_orderid=" + request.ShopOrderId
			    + "&amount=" + request.Amount.ToString("0.##", CultureInfo.InvariantCulture) 
			    + "&currency=" + request.Currency;
			// Remember the terminal
			return new PaymentRequestResult(GetResultFromUrl<PaymentRequestApiResponse>("createPaymentRequest", parameters));
		}
		
		private T GetResultFromUrl<T>(string method, string parameters) where T : ApiResponse, new()
		{
			try
			{
				WebRequest request = WebRequest.Create(String.Format("{0}{1}", _gatewayUrl, method));
				request.Credentials = new NetworkCredential(_username, _password);
				
				HttpWebRequest http = (HttpWebRequest)request;
				http.Method = "POST";
				http.ContentType = "application/x-www-form-urlencoded";
				
				string encodedData = String.Format("terminal={0}{1}", _terminal, parameters);
				Byte[] postBytes = System.Text.Encoding.ASCII.GetBytes(encodedData);
				http.ContentLength = postBytes.Length;
				
				Stream requestStream = request.GetRequestStream();
				requestStream.Write(postBytes, 0, postBytes.Length);
				requestStream.Close();
				
				WebResponse response = request.GetResponse();
				T apiResponse = ConvertXml<T>(response.GetResponseStream());
				return apiResponse;
			}
			catch (Exception exception)
			{
				T response = new T();
				response.Header = new Header();
				if (exception.InnerException != null)
				{
					response.Header.ErrorMessage = exception.Message + "\n" + exception.StackTrace + "\n" + exception.InnerException.Message;
				}
				else
				{
					response.Header.ErrorMessage = exception.Message + "\n" + exception.StackTrace;
				}
				response.Header.ErrorCode = 1;
				return response;
			}
		}

		public T ConvertXml<T>(Stream xml)
		{
			var serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(xml);
		}
	}
}
