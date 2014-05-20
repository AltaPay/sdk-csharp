using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Xml.XPath;
using AltaPay.Service.Dto;
using System.Collections.Generic;


namespace AltaPay.Service
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

		public PaymentResult Reserve(PaymentReservationRequest request) 
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			parameters.Add("terminal", _terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount.GetAmountString());
			parameters.Add("currency", request.Amount.Currency);
			parameters.Add("type", request.PaymentType);

			if (request.CreditCardToken!=null) {
				parameters.Add("credit_card_token", request.CreditCardToken);
			} else {
				parameters.Add("cardnum", request.Pan);
				parameters.Add("emonth", request.ExpiryMonth);
				parameters.Add("eyear", request.ExpiryYear);
			}
			parameters.Add("cvc", request.Cvc);

			if (request.CustomerInfo!=null)
				request.CustomerInfo.AddToDictionary(parameters);

			return new PaymentResult(GetResultFromUrl<APIResponse>("reservationOfFixedAmount", parameters));
		}

		private Dictionary<string,Object> getOrderLines(Dictionary<string,Object> parameters, IList<PaymentOrderLine> orderLines)
		{
			int lineNumber = 0;
			Dictionary<string,Object> orderLinesParam = new Dictionary<string,Object>();
			foreach (PaymentOrderLine orderLine in orderLines)
			{
				Dictionary<string,Object> orderLineParam = new Dictionary<string,Object>();
				orderLineParam.Add("itemId", orderLine.ItemId);
				orderLineParam.Add("quantity", orderLine.Quantity);
				orderLineParam.Add("taxPercent", orderLine.TaxPercent);
				orderLineParam.Add("unitCode", orderLine.UnitCode);
				orderLineParam.Add("unitPrice", orderLine.UnitPrice);
				orderLineParam.Add("description", orderLine.Description);
				orderLineParam.Add("discount", orderLine.Discount);
				orderLineParam.Add("goodsType", orderLine.GoodsType.ToString().ToLower());
				
				orderLinesParam.Add(lineNumber.ToString(), orderLineParam);
				lineNumber++;
			}
			parameters.Add("orderLines", orderLinesParam);
			return parameters;
		}

		public PaymentResult Capture(CaptureRequest request)
		{

			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);
			parameters.Add("amount", request.Amount.GetAmountString());
			if(request.ReconciliationId!=null) parameters.Add("reconciliation_identifier", request.ReconciliationId);
			if (request.InvoiceNumber!=null) parameters.Add("invoice_number", request.InvoiceNumber);
			if (request.SalesTax.HasValue) parameters.Add("sales_tax", request.SalesTax);
			getOrderLines(parameters, request.OrderLines);

			return new PaymentResult(GetResultFromUrl<APIResponse>("captureReservation", parameters));
		}

		public PaymentResult Refund(RefundRequest request) {
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);
			parameters.Add("amount", request.Amount.GetAmountString());
			if (request.ReconciliationId!=null) parameters.Add("reconciliation_identifier", request.ReconciliationId);
			return new PaymentResult(GetResultFromUrl<APIResponse>("refundCapturedReservation", parameters));
		}
		

		public PaymentResult Release(ReleaseRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.PaymentId);
			
			return new PaymentResult(GetResultFromUrl<APIResponse>("releaseReservation", parameters));
		}

		public PaymentResult GetPayment(string paymentId)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", paymentId);
			
			return new PaymentResult(GetResultFromUrl<APIResponse>("transactions", parameters));
		}

		public RecurringResult ChargeSubscription(ChargeSubscriptionRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.SubscriptionId);
			parameters.Add("amount", request.Amount.GetAmountString());

			return new RecurringResult(GetResultFromUrl<APIResponse>("chargeSubscription",parameters));
		}

		public RecurringResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", request.SubscriptionId);
			parameters.Add("amount", request.Amount.GetAmountString());
			
			return new RecurringResult(GetResultFromUrl<APIResponse>("reserveSubscriptionCharge", parameters));
		}
		
		public FundingsResult getFundings(int page)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("page", page);
			return new FundingsResult(GetResultFromUrl<APIResponse>("fundingList",parameters), new NetworkCredential(_username, _password));
		}
		
		public PaymentRequestResult CreatePaymentRequest(PaymentRequest request)
		{
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			
			// Mandatory arguments
			parameters.Add("terminal", request.Terminal);
			parameters.Add("shop_orderid", request.ShopOrderId);
			parameters.Add("amount", request.Amount.GetAmountString());
			parameters.Add("currency", request.Amount.Currency);
			
			// Config
			parameters.Add("config", request.Config.ToDictionary());
			
			// Optional Arguments
			parameters.Add("language", request.Language);
			parameters.Add("transaction_info", request.PaymentInfos);
			parameters.Add("type", request.Type);
			parameters.Add("credit_card_token", request.CreditCardToken);
			parameters.Add("sales_reconciliation_identifier", request.SalesReconciliationIdentifier);
			parameters.Add("sales_invoice_number", request.SalesInvoiceNumber);
			parameters.Add("sales_tax", request.SalesTax);
			parameters.Add("shipping_method", request.ShippingType);
			parameters.Add("cookie", request.Cookie);
			parameters.Add("customer_created_date", request.CustomerCreatedDate);
			parameters.Add("organisation_number", request.OrganisationNumber);
			parameters.Add("account_offer", request.AccountOffer);
			//parameters.Add("fraud_service", request.Config.
			
			// Customer Info
			parameters.Add("customer_info", request.CustomerInfo.AddToDictionary(new Dictionary<string, object>()));


			// Order lines
			parameters = getOrderLines(parameters, request.OrderLines);
			
			return new PaymentRequestResult(GetResultFromUrl<APIResponse>("createPaymentRequest", parameters));
		}
		
		private T GetResultFromUrl<T>(string method, Dictionary<string,Object> parameters) where T : APIResponse, new()
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
