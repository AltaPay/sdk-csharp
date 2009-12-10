using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto
{
	public interface IMotoController
	{
		void PayUsingExistingCreditCard(string cardToken, string cvc);
		void PayUsingNewCreditCard(string pan, int expiryMonth, int expiryYear, string cvc);
		void Cancel();
	}
}
