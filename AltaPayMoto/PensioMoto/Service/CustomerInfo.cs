using System;
using System.Collections.Generic;


namespace AltaPay.Service
{
	public class CustomerInfo
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public string CustomerPhone { get; set; }
		public string BankName { get; set; }
		public string BankPhone { get; set; }
		private CustomerAddress billingAddress = new CustomerAddress();
		private CustomerAddress shippingAddress = new CustomerAddress();
		
		public CustomerAddress BillingAddress
		{
			get { return billingAddress; }
		}
		
		public CustomerAddress ShippingAddress
		{
			get { return shippingAddress; }
		}
		
		public Dictionary<string,object> ToDictionary()
		{
			Dictionary<string,object> parameters = new Dictionary<string,object>();
			
			parameters.Add("email", Email); //The customer's email address.	string
			parameters.Add("username", Username); //The customer's e-shop username.	string
			parameters.Add("customer_phone", CustomerPhone); //The customer's telephone number.	string
			parameters.Add("bank_name", BankName); //The name of the bank where the credit card was issued.	string
			parameters.Add("bank_phone", BankPhone); //The phone number of the bank where the credit card was issued.	String
			
			parameters.Add("billing_firstname", billingAddress.Firstname); //The first name for the customer's billing address.	String
			parameters.Add("billing_lastname", billingAddress.Lastname); //The last name for the customer's billing address.	String
			parameters.Add("billing_city", billingAddress.City); //The city of the customer's billing address.	string
			parameters.Add("billing_region", billingAddress.Region); //The region of the customer's billing address.	string
			parameters.Add("billing_postal", billingAddress.PostalCode); //The postal code of the customer's billing address.	string
			parameters.Add("billing_country", billingAddress.Country); //The country of the customer's billing address as a 2 character ISO-3166 country code.	[a-zA-Z]{2}
			parameters.Add("billing_address", billingAddress.Address); //The street address of the customer's billing address.	string
			
			parameters.Add("shipping_firstname", shippingAddress.Firstname); //The first name for the customer's shipping address.	String
			parameters.Add("shipping_lastname", shippingAddress.Lastname); //The last name for the customer's shipping address.	String
			parameters.Add("shipping_address", shippingAddress.Address); //The street address of the customer's shipping address.	string
			parameters.Add("shipping_city", shippingAddress.City); //The city of the customer's shipping address.	string
			parameters.Add("shipping_region", shippingAddress.Region); //The region of the customer's shipping address.	string
			parameters.Add("shipping_postal", shippingAddress.PostalCode); //The postal code of the customer's shipping address.	string
			parameters.Add("shipping_country", shippingAddress.Country); //The country of the customer's shipping address as a 2 character ISO-3166 country code.	[a-zA-Z]{2}
			
			return parameters;
		}
	}
}

