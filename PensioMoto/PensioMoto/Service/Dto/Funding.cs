using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Data;
using AltaPay.Moto.Com;
using AltaPay.Moto;

namespace AltaPay.Service
{
	public class Funding : IFunding
	{
		[XmlElement("Filename")]
		public string Id { get;set; }
		public string ContractIdentifier { get; set; }
		[XmlArrayItem(ElementName = "Shop")]
		public string[] Shops { get; set; }
		public string Acquirer { get; set; }
		public string FundingDate { get; set; }
		public string Amount { get; set; }
		public string CreatedDate { get; set; }
		public string DownloadLink { get; set; }

		private List<IFundingLine> lines = null;
		private NetworkCredential networkCredential;

		public string ShopString
		{
			get
			{
				return String.Join(", ", Shops);
			}
		}

		public int getLineCount()
		{
			EnsureFileIsLoaded();
			return lines.Count;
		}

		public IFundingLine getLine(int i)
		{
			EnsureFileIsLoaded();
			return lines[i];
		}

		private void EnsureFileIsLoaded()
		{
			if (lines == null)
			{
				WebRequest request = WebRequest.Create(new Uri(DownloadLink));
				request.Credentials = networkCredential;
				
				WebResponse response = request.GetResponse();

				CSVReader reader = new CSVReader(new StreamReader(response.GetResponseStream()));
				DataTable table = reader.CreateDataTable(true);
				lines = new List<IFundingLine>();
				foreach (DataRow dr in table.Rows)
				{
					//string hat = getDataString(dr, "Exchange Rate");
					FundingLine line = new FundingLine
					{
						Date = getDataString(dr,"Date")
						, Type = getDataString(dr,"Type")
						, Id = getDataString(dr,"ID")
						, ReconciliationIdentifer = getDataString(dr,"Reconciliation Identifier")
						, PaymentId = getDataString(dr,"Payment")
						, Order = getDataString(dr,"Order")
						, Terminal = getDataString(dr,"Terminal")
						, Shop = getDataString(dr,"Shop")
						, TransactionCurrency = getDataString(dr,"Transaction Currency")
						, TransactionAmount = getDataString(dr,"Transaction Amount")
						, ExchangeRate = getDataString(dr,"Exchange Rate")
						, SettlementCurrency = getDataString(dr,"Settlement Currency")
						, SettlementAmount = getDataString(dr,"Settlement Amount")
						, FixedFee = getDataString(dr,"Fixed Fee")
						, FixedFeeVAT = getDataString(dr,"Fixed Fee VAT")
						, RateBasedFee = getDataString(dr,"Rate Based Fee")
						, RateBasedFeeVAT = getDataString(dr,"Rate Based Fee VAT")
					};

					lines.Add(line);
				}
			}
		}

		private string getDataString(DataRow dr, string column)
		{
			if (dr.Table.Columns.Contains(column))
			{
				try
				{
					if(dr[column].GetType() == typeof(Single))
					{
						return dr.Field<Single>(column).ToString("F");
					}
					return dr.Field<String>(column);
				}
				catch(Exception)
				{
					return dr[column].ToString();
				}
			}
			else
			{
				return null;
			}
		}

		internal void inject(NetworkCredential networkCredential)
		{
			this.networkCredential = networkCredential;
		}
	}
}
