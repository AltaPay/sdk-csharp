using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AltaPay.Service.Dto;
using System.Net;

namespace AltaPay.Service
{
	public class FundingContentResult
	{
		public String fundingContent { get; set; }

		public FundingContentResult(String url, NetworkCredential networkCredential)
		{
			WebRequest request = WebRequest.Create (url);
			request.Credentials = networkCredential;
			WebResponse response = request.GetResponse ();
			Stream dataStream = response.GetResponseStream ();
			StreamReader reader = new StreamReader (dataStream);
			fundingContent = reader.ReadToEnd ();
			reader.Close ();
			response.Close ();
		}
	}
}

