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
		
		private bool isInitialized = false;

		public void Initialize(string gatewayUrl, string username, string password, string terminal)
		{
			_gatewayUrl = gatewayUrl;
			_terminal = terminal;
			_username = username;
			_password = password;
			
			isInitialized = true;
		}

		public ReserveResult Reserve(ReserveRequest param) 
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();

			parameters.Add("terminal", _terminal);
			parameters.Add("shop_orderid", param.ShopOrderId);
			parameters.Add("amount", param.Amount.GetAmountString());
			parameters.Add("currency", param.Amount.Currency.GetNumericString());
			parameters.Add("type", param.PaymentType);
			parameters.Add("payment_source", param.Source);

			if (param.CreditCardToken!=null) {
				parameters.Add("credit_card_token", param.CreditCardToken);
			} else {
				parameters.Add("cardnum", param.Pan);
				parameters.Add("emonth", param.ExpiryMonth);
				parameters.Add("eyear", param.ExpiryYear);
			}
			parameters.Add("cvc", param.Cvc);

			if (param.CustomerInfo!=null)
				param.CustomerInfo.AddToDictionary(parameters);

			return new ReserveResult(GetResponseFromApiCall("reservationOfFixedAmount", parameters));
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

		public CaptureResult Capture(CaptureRequest param)
		{
			ThrowExceptionIfNotInitialized();

			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", param.PaymentId);
			parameters.Add("amount", param.Amount.GetAmountString());
			if(param.ReconciliationId!=null) parameters.Add("reconciliation_identifier", param.ReconciliationId);
			if (param.InvoiceNumber!=null) parameters.Add("invoice_number", param.InvoiceNumber);
			if (param.SalesTax.HasValue) parameters.Add("sales_tax", param.SalesTax);
			getOrderLines(parameters, param.OrderLines);

			return new CaptureResult(GetResponseFromApiCall("captureReservation", parameters));
		}

		public RefundResult Refund(RefundRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", param.PaymentId);
			parameters.Add("amount", param.Amount.GetAmountString());
			if (param.ReconciliationId!=null) parameters.Add("reconciliation_identifier", param.ReconciliationId);
			return new RefundResult(GetResponseFromApiCall("refundCapturedReservation", parameters));
		}
		

		public ReleaseResult Release(ReleaseRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", param.PaymentId);
			
			return new ReleaseResult(GetResponseFromApiCall("releaseReservation", parameters));
		}

		public GetPaymentResult GetPayment(GetPaymentRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", param.PaymentId);
			
			return new GetPaymentResult(GetResponseFromApiCall("transactions", parameters));
		}

		public ChargeSubscriptionResult ChargeSubscription(ChargeSubscriptionRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", param.SubscriptionId);
			parameters.Add("amount", param.Amount.GetAmountString());

			return new ChargeSubscriptionResult(GetResponseFromApiCall("chargeSubscription",parameters));
		}

		public ReserveSubscriptionChargeResult ReserveSubscriptionCharge(ReserveSubscriptionChargeRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("transaction_id", param.SubscriptionId);
			parameters.Add("amount", param.Amount.GetAmountString());
			
			return new ReserveSubscriptionChargeResult(GetResponseFromApiCall("reserveSubscriptionCharge", parameters));
		}
		
		public FundingsResult GetFundings(GetFundingsRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			parameters.Add("page", param.Page);
			return new FundingsResult(GetResponseFromApiCall("fundingList",parameters), new NetworkCredential(_username, _password));
		}
		
		public PaymentRequestResult CreatePaymentRequest(PaymentRequestRequest param)
		{
			ThrowExceptionIfNotInitialized();
			
			Dictionary<string,Object> parameters = new Dictionary<string, Object>();
			
			// Mandatory arguments
			parameters.Add("terminal", param.Terminal);
			parameters.Add("shop_orderid", param.ShopOrderId);
			parameters.Add("amount", param.Amount.GetAmountString());
			parameters.Add("currency", param.Amount.Currency.GetNumericString());
			
			// Config
			parameters.Add("config", param.Config.ToDictionary());
			
			// Optional Arguments
			parameters.Add("language", param.Language);
			parameters.Add("transaction_info", param.PaymentInfos);
			parameters.Add("type", param.Type);
			parameters.Add("credit_card_token", param.CreditCardToken);
			parameters.Add("sales_reconciliation_identifier", param.SalesReconciliationIdentifier);
			parameters.Add("sales_invoice_number", param.SalesInvoiceNumber);
			parameters.Add("sales_tax", param.SalesTax);
			parameters.Add("shipping_method", param.ShippingType);
			parameters.Add("cookie", param.Cookie);
			parameters.Add("customer_created_date", param.CustomerCreatedDate);
			parameters.Add("organisation_number", param.OrganisationNumber);
			parameters.Add("account_offer", param.AccountOffer);
			//parameters.Add("fraud_service", request.Config.
			
			// Customer Info
			parameters.Add("customer_info", param.CustomerInfo.AddToDictionary(new Dictionary<string, object>()));


			// Order lines
			parameters = getOrderLines(parameters, param.OrderLines);
			
			return new PaymentRequestResult(GetResponseFromApiCall("createPaymentRequest", parameters));
		}


		public ApiResult ParsePostBackXmlResponse(string responseStr)
		{
			using (Stream stream = new MemoryStream()) {
				StreamWriter writer = new StreamWriter(stream);
				writer.Write(responseStr);
				writer.Flush();
				stream.Position = 0;
				return ParsePostBackXmlResponse(stream);
			}
		}


		public ApiResult ParsePostBackXmlResponse(Stream responseStream)
		{
			// Get the apiResponse
			APIResponse apiResponse = GetApiResponse(responseStream);
			if (apiResponse.Header.ErrorCode!=0)
				throw new Exception("Invalid response : " + apiResponse.Header.ErrorMessage);


			// Detect auth type 
			if (apiResponse.Body.Transactions.Length==0)
				throw new Exception("The response contains no transactions");
			string authType = apiResponse.Body.Transactions[0].AuthType;

			// Wrap Api Respons to proper result
			switch (authType) 
			{
				case "payment":
				case "paymentAndCapture":
				case "recurring":
				case "subscription":
				case "verifyCard":
					return new ReserveResult(apiResponse);

				case "subscriptionAndCharge":
				case "recurringAndCapture":
					return new SubscriptionResult(apiResponse);

				default: 
					throw new Exception("Unhandled Authtype : " + authType);
			}
		}

		private APIResponse GetApiResponse(Stream stream)
		{
			try
			{
				return ConvertXml<APIResponse>(stream);
			}
			catch (Exception exception)
			{
				APIResponse response = new APIResponse();
				response.Header = new Header();

				response.Header.ErrorMessage = exception.Message + "\n" + exception.StackTrace;
				if (exception.InnerException != null)
					response.Header.ErrorMessage += "\n" + exception.InnerException.Message;

				response.Header.ErrorCode = 1;
				return response;
			}
		}

		private APIResponse GetResponseFromApiCall(string method, Dictionary<string,Object> parameters)
		{
			using (Stream responseStream = CallApi(method, parameters))
			{
				return GetApiResponse(responseStream);
			}
		}

		private Stream CallApi(string method, Dictionary<string,Object> parameters)
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
			return response.GetResponseStream();
		}

		private T ConvertXml<T>(Stream xml)
		{
			var serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(xml);
		}
		
		private void ThrowExceptionIfNotInitialized()
		{
			if (!isInitialized)
			{
				throw new InvalidOperationException("You must call initialize before using the API");
			}
		}
	}
}
