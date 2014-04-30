using System;

namespace PensioMoto.Service
{
	public class CustomerInfo
	{
		public string Email { get; set; }
		public string Username { get; set; }
		public string CustomerPhone { get; set; }
		//public string BankName { get; set; }
		//public string BankPhone { get; set; }
		private CustomerAddress BillingAddress = new CustomerAddress();
		private CustomerAddress ShippingAddress = new CustomerAddress();
		public string CustomerCreatedDate { get; set; }
		
		public CustomerAddress GetBillingAddress()
		{
			return BillingAddress;
		}
		
		public CustomerAddress GetShippingAddress()
		{
			return ShippingAddress;
		}
	}
}

