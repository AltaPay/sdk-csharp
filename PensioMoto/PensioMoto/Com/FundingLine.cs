using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PensioMoto
{
	public class FundingLine
		: IFundingLine
	{
		public string Date { get; set; }
		public string Type { get; set; }
		public string Id { get; set; }
		public string ReconciliationIdentifer { get; set; }
		public string PaymentId { get; set; }
		public string Order { get; set; }
		public string Terminal { get; set; }
		public string Shop { get; set; }
		public string TransactionCurrency { get; set; }
		public string TransactionAmount { get; set; }
		public string ExchangeRate { get; set; }
		public string SettlementCurrency { get; set; }
		public string SettlementAmount { get; set; }
		public string FixedFee { get; set; }
		public string FixedFeeVAT { get; set; }
		public string RateBasedFee { get; set; }
		public string RateBasedFeeVAT { get; set; }
	}
}
