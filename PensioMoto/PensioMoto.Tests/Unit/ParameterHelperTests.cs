using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PensioMoto.Tests.Unit
{
	[TestFixture]
	public class ParameterHelperTests
	{
		private ParameterHelper _helper;
		Dictionary<string,Object> _parameters;

		[SetUp]
		public void Setup()
		{
			_helper = new ParameterHelper();
			_parameters = new Dictionary<string,Object>();
		}

		[Test]
		public void EmptyDictionaryReturnsEmptyString()
		{
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("", paramString);
		}

		[Test]
		public void SimpleDictionaryReturnsOneParam()
		{
			_parameters.Add("k1", "v1");
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("k1=v1", paramString);
		}

		[Test]
		public void MultipleAreSeparatedWithAmpersand()
		{
			_parameters.Add("k1", "v1");
			_parameters.Add("k2", "v2");
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("k1=v1&k2=v2", paramString);
		}

		[Test]
		public void KeysAndValuesAreURLEncoded()
		{
			_parameters.Add("k 1", "v 1");
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("k+1=v+1", paramString);
		}

		[Test]
		public void DoublesAreConvertedToTwoDecimals()
		{
			_parameters.Add("double", 12324.1);
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("double=12324.10", paramString);
		}

		[Test]
		public void EmptyNestedDictionaryIsIgnored()
		{
			_parameters.Add("k1", "v1");
			Dictionary<string,Object> nestedDict = new Dictionary<string,Object>();
			_parameters.Add("nested", nestedDict);
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("k1=v1", paramString);
		}

		[Test]
		public void NestedDictionaryIsAddedAsPHPAssocArray()
		{
			_parameters.Add("k1", "v1");
			Dictionary<string,Object> nestedDict = new Dictionary<string,Object>();
			nestedDict.Add("nk1", "nv1");
			_parameters.Add("nested", nestedDict);
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("k1=v1&nested[nk1]=nv1", paramString);
		}

		[Test]
		public void DoubleNestedDictionaryIsAddedAsPHPAssocArray()
		{
			_parameters.Add("k1", "v1");
			Dictionary<string,Object> nestedDict = new Dictionary<string,Object>();
			nestedDict.Add("nk1", "nv1");
			Dictionary<string,Object> nestedDict2 = new Dictionary<string,Object>();
			nestedDict2.Add("n2k1", "n2v1");
			nestedDict.Add("nested2", nestedDict2);
			_parameters.Add("nested", nestedDict);
			string paramString = _helper.Convert(_parameters);

			Assert.AreEqual("k1=v1&nested[nk1]=nv1&nested[nested2][n2k1]=n2v1", paramString);
		}
	}
}
