using System;
using System.Linq;
using AltaPay;
using NUnit.Framework;
using AltaPay.Service;

namespace AltaPay.Service.Tests.Unit
{
	public class AmountTests
	{
		[Test]
		public void CreationFromDecimals()
		{
			Assert.AreEqual(100m, Amount.Get(100m, Currency.DKK).Value);		
			Assert.AreEqual(Currency.AED, Amount.Get(100m, Currency.AED).Currency);		
		}

		[Test]
		public void CreationFromLong()
		{
			Assert.AreEqual(100, Amount.Get(100, Currency.DKK).Value);		
			Assert.AreEqual(Currency.AED, Amount.Get(100, Currency.AED).Currency);		
		}

		[Test]
		public void CreationFromDouble()
		{
			Assert.AreEqual(100.12d, Amount.Get(100.12d, Currency.DKK).Value);		
			Assert.AreEqual(Currency.AED, Amount.Get(100.12d, Currency.AED).Currency);		
		}
		
		[Test]
		public void GetAmountString()
		{
			Assert.AreEqual("1.23", Amount.Get(1.233m, Currency.DKK).GetAmountString());		
			Assert.AreEqual("42.00", Amount.Get(42, Currency.DKK).GetAmountString());		
			Assert.AreEqual("100", Amount.Get(100.12345m, Currency.VUV).GetAmountString()); 
			Assert.AreEqual("42.123", Amount.Get(42.123, Currency.JOD).GetAmountString());		
			Assert.AreEqual("1000000000.12", Amount.Get(1000000000.12m, Currency.DKK).GetAmountString()); 
		}
		
		[Test]
		public void ToStringTest()
		{
			Assert.AreEqual("1.23 DKK", Amount.Get(1.23, Currency.DKK).ToString());		
			Assert.AreEqual("100.12 DKK", Amount.Get(100.12345, Currency.DKK).ToString());
		}
		
		
	}
}

