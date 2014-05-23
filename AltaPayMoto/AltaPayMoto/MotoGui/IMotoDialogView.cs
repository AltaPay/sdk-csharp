using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;
using System.Windows.Forms;

namespace AltaPay.Moto
{
	public interface IMotoDialogView
	{
		void Initialize(IMotoController controller, string orderId, double amount, AuthType paymentType);
		void AddCreditCard(string maskedPan, string cardToken);
		void SetAvsInfo(string firstName, string lastName, string address, string postalCode, string city, string region, string country, string phone, string email);
		void ShowBlocking();
		void Close();
		void EnableView(string status);
	}
}
