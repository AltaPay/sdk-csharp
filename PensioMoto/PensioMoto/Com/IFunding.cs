using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PensioMoto.Com
{
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IFunding
	{
		string Id { get;  }
		string ContractIdentifier { get;}
		string ShopString { get; }
		string Acquirer { get; }
		string FundingDate { get; }
		string Amount { get; }
		string CreatedDate { get; }
		string DownloadLink { get; }
	}
}
