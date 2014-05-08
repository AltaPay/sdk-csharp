using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using System.Runtime.InteropServices;

namespace AltaPay.Moto
{
	public interface IMotoDialog
	{
		void Initialize(
			string gatewayUrl, 
			string apiUsername, 
			string apiPassword,
			string terminal, 
			string orderId,
			double amount, 
			int currency, 
			PaymentType paymentType);
		void AddCreditCard(string maskedPan, string cardToken);

		void SetAvsInfo(string firstName, string lastName, string address, string postalCode, string city, string region, string country, string phone, string email);

		PaymentResult Show();
	}
}
