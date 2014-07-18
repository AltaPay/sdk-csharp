using System;
using System.Globalization;

namespace AltaPay
{
	internal static class Globalisation
	{
		public static NumberFormatInfo AmountNumberFormat
		{
			get
			{
				return (new CultureInfo("en-US")).NumberFormat;
			}
		}
	}
}

