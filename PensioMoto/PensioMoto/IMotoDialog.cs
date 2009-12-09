using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[GuidAttribute("1d64f373-acc8-406a-84cb-c7671ff65a5a")]
	[ComVisible(true)]
	public interface IMotoDialog
	{
		[DispIdAttribute(1)]
		void Initialize(string gatewayUrl, string apiUsername, string apiPassword,
			string terminal, string orderId,
			float amount, int currency, PaymentType paymentType);
		[DispIdAttribute(2)]
		void SetCreditCard(string maskedPan, string cardToken);
		[DispIdAttribute(3)]
		PaymentResult Show();
	}
}
