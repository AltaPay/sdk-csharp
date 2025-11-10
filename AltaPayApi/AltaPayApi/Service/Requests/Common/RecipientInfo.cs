using System;
using System.Collections.Generic;
using AltaPay.Service.Dto;


namespace AltaPay.Service
{
	public class RecipientInfo
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public string AccountIdentifier { get; set; }
		public string CustomerPhone { get; set; }
		public CustomerAddress BillingAddress { get; set; }
		public CustomerAddress ShippingAddress { get; set; }
		public DateTime BirthDate { get; set; }
		public Gender Gender { get; set; }

		public RecipientInfo ()
		{
			BillingAddress = new CustomerAddress ();
			ShippingAddress = new CustomerAddress ();
		}

		public Dictionary<string, object> AddToDictionary (Dictionary<string, object> parameters)
		{
			parameters.Add ("email", Email); //The recipient's email address.	string
			parameters.Add ("username", Username); //The recipient's e-shop username.	string
			parameters.Add ("account_identifier", AccountIdentifier); //The recipient's account identifier. It can be name of customer's account or pan.	string
			parameters.Add ("customer_phone", CustomerPhone); //The recipient's telephone number.	string
			if (BirthDate.Ticks != 0) {
				parameters.Add ("birthdate", BirthDate.ToString ("yyyy-MM-dd")); //The recipient's birth date in format yyyy-MM-dd
            }
            parameters.Add ("gender", Gender.ToString ().ToLower ()); //Gender of the recipient.FMmalefemale
			parameters.Add ("billing_firstname", BillingAddress.Firstname); //The first name for the recipient's billing address.	String
			parameters.Add ("billing_lastname", BillingAddress.Lastname); //The last name for the recipient's billing address.	String
			parameters.Add ("billing_city", BillingAddress.City); //The city of the recipient's billing address.	string
			parameters.Add ("billing_region", BillingAddress.Region); //The region of the recipient's billing address.	string
			parameters.Add ("billing_postal", BillingAddress.PostalCode); //The postal code of the recipient's billing address.	string
			parameters.Add ("billing_country", BillingAddress.Country); //The country of the recipient's billing address as a 2 character ISO-3166 country code.	[a-zA-Z]{2}
			parameters.Add ("billing_address", BillingAddress.Address); //The street address of the recipient's billing address.	string

			parameters.Add ("shipping_firstname", ShippingAddress.Firstname); //The first name for the recipient's shipping address.	String
			parameters.Add ("shipping_lastname", ShippingAddress.Lastname); //The last name for the recipient's shipping address.	String
			parameters.Add ("shipping_address", ShippingAddress.Address); //The street address of the recipient's shipping address.	string
			parameters.Add ("shipping_city", ShippingAddress.City); //The city of the recipient's shipping address.	string
			parameters.Add ("shipping_region", ShippingAddress.Region); //The region of the recipient's shipping address.	string
			parameters.Add ("shipping_postal", ShippingAddress.PostalCode); //The postal code of the recipient's shipping address.	string
			parameters.Add ("shipping_country", ShippingAddress.Country); //The country of the recipient's shipping address as a 2 character ISO-3166 country code.	[a-zA-Z]{2}

			return parameters;
		}
	}
}

