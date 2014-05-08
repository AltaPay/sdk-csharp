using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AltaPay.Moto.Com
{
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IFundingLine
	{
		string Date { get; }
		string Type { get; }
		string Id { get; }
		string ReconciliationIdentifer { get; }
		string PaymentId { get; }
		string Order { get; }
		string Terminal { get; }
		string Shop { get; }
		string TransactionCurrency { get; }
		string TransactionAmount { get; }
		string ExchangeRate { get; }
		string SettlementCurrency { get; }
		string SettlementAmount { get; }
		string FixedFee { get; }
		string FixedFeeVAT { get; }
		string RateBasedFee { get; }
		string RateBasedFeeVAT { get; }
	}

}
