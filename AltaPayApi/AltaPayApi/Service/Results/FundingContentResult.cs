using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;
using System.Net;
using LumenWorks.Framework.IO.Csv;

namespace AltaPay.Service
{
	public class FundingContentResult
	{

		private const char csv_delimiter = ';';

		public String FundingContent { get; set; }

		public FundingContentResult(String url, NetworkCredential networkCredential)
		{
			WebRequest request = WebRequest.Create (url);
			request.Credentials = networkCredential;
			WebResponse response = request.GetResponse ();
			Stream dataStream = response.GetResponseStream ();
			StreamReader reader = new StreamReader (dataStream);
			FundingContent = reader.ReadToEnd ();
			reader.Close ();
			response.Close ();
		}

		public List<FundingRecord> GetFundingRecordList ()
		{
			List<FundingRecord> records = new List<FundingRecord>();

			using (CsvReader csv = new CsvReader(GenerateStreamFromString(this.FundingContent), true, csv_delimiter))
			{
				while (csv.ReadNextRecord())
				{
					records.Add(new FundingRecord(csv));
				}
			}

			return records;
		}

		public StreamReader GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return new StreamReader(stream);
		}
			
	}
}

