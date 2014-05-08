using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IComFundingsResult
	{
		string Result { get; set; }
		string ResultMessage { get; set; }
		int Pages { get; set; }

		int getFundingCount();
		IFunding getFunding(int i);
	}
}
