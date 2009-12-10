using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto
{
	public interface IMotoController
	{
		void PayUsingExistingCreditCard(string cardToken, int cvc);
		void PayUsingNewCreditCard(string pan, int expiryMonth, int expiryYear, int cvc);
		void Cancel();
	}
}
