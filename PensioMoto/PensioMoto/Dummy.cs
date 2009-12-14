using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[GuidAttribute("636dd551-a635-4334-b793-8eaae5efa4ff")]
	public class Dummy
	{
		[DispIdAttribute(1)]
		public int MyMethod(int i)
		{
			return i + 1;
		}
	}
}
