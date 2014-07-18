using System;
using NUnit.Framework;
using System.Globalization;

namespace AltaPay.Api.Tests
{
	public abstract class BaseTest
	{
		[TestFixtureSetUp]
		public void FixtureSetup()
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("da-DK");
		}
	}
}

