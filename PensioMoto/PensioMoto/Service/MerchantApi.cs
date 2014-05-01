using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Xml.XPath;
using PensioMoto.Service.Dto;
using System.Collections.Generic;


namespace PensioMoto.Service
{
	public class MerchantApi : IMerchantApi
	{
		// Dependency
		private ParameterHelper ParameterHelper = new ParameterHelper();
		
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
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("terminal", _terminal);
			parameters.Add("shop_orderid", shopOrderId);
			parameters.Add("amount", amount);
			parameters.Add("currency", currency);
			parameters.Add("type", paymentType);
			parameters.Add("cardnum", pan);
			parameters.Add("emonth", expiryMonth);
			parameters.Add("eyear", expiryYear);
			parameters.Add("cvc", cvc);
			parameters = getAvsInfoParameters(parameters, avsInfo);

			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("reservationOfFixedAmountMOTO", parameters));
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
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("terminal", _terminal);
			parameters.Add("shop_orderid", shopOrderId);
			parameters.Add("amount", amount);
			parameters.Add("currency", currency);
			parameters.Add("type", paymentType);
			parameters.Add("credit_card_token", creditCardToken);
			parameters.Add("cvc", cvc);
			parameters = getAvsInfoParameters(parameters, avsInfo);

			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("reservationOfFixedAmountMOTO", parameters));
		}
		
		private Dictionary<string,Object> getAvsInfoParameters(Dictionary<string,Object> parameters, AvsInfo avsInfo)
		{
			if (avsInfo != null)
			{
				parameters.Add("billing_firstname", avsInfo.FirstName);
				parameters.Add("billing_lastname", avsInfo.LastName);
				parameters.Add("billing_address", avsInfo.Address);
				parameters.Add("billing_city", avsInfo.City);
				parameters.Add("billing_country", avsInfo.Country);
				parameters.Add("billing_region", avsInfo.Region);
				parameters.Add("billing_postal", avsInfo.PostalCode);
				parameters.Add("email", avsInfo.Email);
				parameters.Add("customer_phone", avsInfo.Phone);
			}
			return parameters;
		}
		
		private Dictionary<string,Object> getPaymentDetailsParameters(Dictionary<string,Object> parameters, PaymentDetails paymentDetails)
		{
			int lineNumber = 0;
			Dictionary<string,Object> orderLinesParam = new Dictionary<string,Object>();
			foreach (PaymentOrderLine orderLine in paymentDetails.getLines())
			{
				Dictionary<string,Object> orderLineParam = new Dictionary<string,Object>();
				orderLineParam.Add("itemId", orderLine.ItemId);
				orderLineParam.Add("quantity", orderLine.Quantity);
				orderLineParam.Add("taxPercent", orderLine.TaxPercent);
				orderLineParam.Add("unitCode", orderLine.UnitCode);
				orderLineParam.Add("unitPrice", orderLine.UnitPrice);
				orderLineParam.Add("description", orderLine.Description);
				orderLineParam.Add("discount", orderLine.Discount);
				orderLineParam.Add("goodsType", orderLine.GoodsType);
				
				orderLinesParam.Add(lineNumber.ToString(), orderLineParam);
				lineNumber++;
			}
			parameters.Add("orderLines", orderLinesParam);

			if (paymentDetails.ReconciliationIdentifier != null)
			{
				parameters.Add ("reconciliation_identifier", paymentDetails.ReconciliationIdentifier);
			}

			if (paymentDetails.InvoiceNumber != null)
			{
				parameters.Add("invoice_number", paymentDetails.InvoiceNumber);
			}

			if (paymentDetails.SalesTax != 0)
			{
				parameters.Add("sales_tax", paymentDetails.SalesTax);
			}
			return parameters;
		}

		public PaymentResult Capture(string paymentId, double amount, string reconciliationIdentifier)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			parameters.Add("amount", amount);
			parameters.Add("reconciliation_identifier", reconciliationIdentifier);
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("captureReservation", parameters));
		}

		public PaymentResult Capture(string paymentId, double amount)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			parameters.Add("amount", amount);
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("captureReservation", parameters));
		}
		
		public PaymentResult Capture(string paymentId, double amount, PaymentDetails paymentDetails)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			parameters.Add("amount", amount);
			parameters = getPaymentDetailsParameters(parameters, paymentDetails);
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("captureReservation", parameters));
		}

		public PaymentResult Refund(string paymentId, double amount, string reconciliationIdentifier)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			parameters.Add("amount", amount);
			parameters.Add("reconciliation_identifier", reconciliationIdentifier);
			
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("refundCapturedReservation", parameters));
		}

		public PaymentResult Refund(string paymentId, double amount)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			parameters.Add("amount", amount);
			
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("refundCapturedReservation", parameters));
		}

		public PaymentResult Release(string paymentId)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("releaseReservation", parameters));
		}

		public SplitPaymentResult Split(string paymentId, double amount)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			parameters.Add("amount", amount);
			
			return new SplitPaymentResult(GetResultFromUrl<PaymentApiResponse>("splitTransaction", parameters));
		}

		public PaymentResult GetPayment(string paymentId)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			
			return new PaymentResult(GetResultFromUrl<PaymentApiResponse>("transactions", parameters));
		}

		public RecurringResult CaptureRecurring(string recurringPaymentId, double amount)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", recurringPaymentId);
			parameters.Add("amount", amount);
			
			return new RecurringResult(GetResultFromUrl<PaymentApiResponse>("captureRecurring",parameters));
		}

		public RecurringResult PreauthRecurring(string recurringPaymentId, double amount)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", recurringPaymentId);
			parameters.Add("amount", amount);
			
			return new RecurringResult(GetResultFromUrl<PaymentApiResponse>("preauthRecurring", parameters));
		}
		
		public FundingsResult getFundings(int page)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("page", page);
			return new FundingsResult(GetResultFromUrl<FundingsApiResponse>("fundingList",parameters), new NetworkCredential(_username, _password));
		}
		
		public PaymentRequestResult CreatePaymentRequest(PaymentRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount);
			parameters.Add("currency", request.Currency);
			return new PaymentRequestResult(GetResultFromUrl<PaymentRequestApiResponse>("createPaymentRequest", parameters));
		}
		
		private T GetResultFromUrl<T>(string method, Dictionary<string,Object> parameters) where T : ApiResponse, new()
		{
			try
			{
				WebRequest request = WebRequest.Create(String.Format("{0}{1}", _gatewayUrl, method));
				request.Credentials = new NetworkCredential(_username, _password);
				
				HttpWebRequest http = (HttpWebRequest)request;
				http.Method = "POST";
				http.ContentType = "application/x-www-form-urlencoded";
				
				string encodedData = ParameterHelper.Convert(parameters);
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
