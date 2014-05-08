using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaPay.Service;

namespace AltaPay.Moto
{
	public interface IMotoController
	{
		void PayUsingExistingCreditCard(string cardToken, string cvc, AvsInfo avsInfo);
		void PayUsingNewCreditCard(string pan, int expiryMonth, int expiryYear, string cvc, AvsInfo avsInfo);
		void Cancel();
	}
}
