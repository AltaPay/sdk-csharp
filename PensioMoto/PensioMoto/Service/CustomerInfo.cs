using System;
using System.Collections.Generic;


namespace PensioMoto.Service
{
	public class CustomerInfo
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public string CustomerPhone { get; set; }
		public string BankName { get; set; }
		public string BankPhone { get; set; }
		private CustomerAddress BillingAddress = new CustomerAddress();
		private CustomerAddress ShippingAddress = new CustomerAddress();
		
		public CustomerAddress GetBillingAddress()
		{
			return BillingAddress;
		}
		
		public CustomerAddress GetShippingAddress()
		{
			return ShippingAddress;
		}
		
		public Dictionary<string,object> ToDictionary()
		{
			Dictionary<string,object> parameters = new Dictionary<string,object>();
			
			parameters.Add("email", Email); //The customer's email address.	string
			parameters.Add("username", Username); //The customer's e-shop username.	string
			parameters.Add("customer_phone", CustomerPhone); //The customer's telephone number.	string
			parameters.Add("bank_name", BankName); //The name of the bank where the credit card was issued.	string
			parameters.Add("bank_phone", BankPhone); //The phone number of the bank where the credit card was issued.	String
			
			parameters.Add("billing_firstname", GetBillingAddress().Firstname); //The first name for the customer's billing address.	String
			parameters.Add("billing_lastname", GetBillingAddress().Lastname); //The last name for the customer's billing address.	String
			parameters.Add("billing_city", GetBillingAddress().City); //The city of the customer's billing address.	string
			parameters.Add("billing_region", GetBillingAddress().Region); //The region of the customer's billing address.	string
			parameters.Add("billing_postal", GetBillingAddress().PostalCode); //The postal code of the customer's billing address.	string
			parameters.Add("billing_country", GetBillingAddress().Country); //The country of the customer's billing address as a 2 character ISO-3166 country code.	[a-zA-Z]{2}
			parameters.Add("billing_address", GetBillingAddress().Address); //The street address of the customer's billing address.	string
			
			parameters.Add("shipping_firstname", GetShippingAddress().Firstname); //The first name for the customer's shipping address.	String
			parameters.Add("shipping_lastname", GetShippingAddress().Lastname); //The last name for the customer's shipping address.	String
			parameters.Add("shipping_address", GetShippingAddress().Address); //The street address of the customer's shipping address.	string
			parameters.Add("shipping_city", GetShippingAddress().City); //The city of the customer's shipping address.	string
			parameters.Add("shipping_region", GetShippingAddress().Region); //The region of the customer's shipping address.	string
			parameters.Add("shipping_postal", GetShippingAddress().PostalCode); //The postal code of the customer's shipping address.	string
			parameters.Add("shipping_country", GetShippingAddress().Country); //The country of the customer's shipping address as a 2 character ISO-3166 country code.	[a-zA-Z]{2}
			
			return parameters;
		}
	}
}

