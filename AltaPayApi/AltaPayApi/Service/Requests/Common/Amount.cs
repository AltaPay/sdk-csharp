using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace AltaPay.Service
{
	public class Amount
	{
		public decimal Value { get; private set;}
		public Currency Currency { get; private set; }
		
		private Amount(decimal _value, Currency currency) {
			Value = _value; 
			Currency = currency;
		}
		
		public String GetAmountString() 
		{
			switch(Currency.Decimals) {
				case(0): return Value.ToString("0", Globalisation.AmountNumberFormat);
				case(1): return Value.ToString(".0", Globalisation.AmountNumberFormat); 
				case(2): return Value.ToString(".00", Globalisation.AmountNumberFormat); 
				case(3): return Value.ToString(".000", Globalisation.AmountNumberFormat); 
			};

			throw new Exception("Unsupported number of decimals : " + Currency.Decimals);
		}
		
		public override string ToString()
		{
			return String.Format("{0} {1}", GetAmountString(), Currency);
		}

		public static Amount Get(decimal _value, Currency currency) 
		{
			return new Amount(_value, currency);
		}
		
		public static Amount Get(long _value, Currency currency) 
		{
			return new Amount(_value, currency);
		}

		public static Amount Get(double _value, Currency currency) 
		{
			return new Amount((decimal)_value, currency);
		}

	
	}
}

