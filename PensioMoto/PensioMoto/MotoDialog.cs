using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;

namespace PensioMoto
{
	[GuidAttribute("0e04ff52-3f37-481c-b11d-ebcfcd91c4e8")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class MotoDialog : IMotoDialog
	{
		private string _terminal;
		private string _orderId;
		private float _amount;
		private PaymentType _paymentType;

		public void Initialize(string gatewayUrl, 
			string apiUsername, 
			string apiPassword, 
			string terminal, 
			string orderId, 
			float amount, 
			int currency, 
			PaymentType paymentType)
		{
			_orderId = orderId;
			_amount = amount;
			_paymentType = paymentType;
			_terminal = terminal;
		}

		public void SetCreditCard(string maskedPan, string cardToken)
		{
			
		}

		public PaymentResult Show()
		{
			return new PaymentResult()
			{
				Result = Result.Success,
				Payment = new Payment
				{
					ShopOrderId = _orderId,
					PaymentId = new Random().Next(),
					CapturedAmount = _paymentType == PaymentType.paymentAndCapture ? _amount : 0,
					ReservedAmount = _paymentType == PaymentType.recurring ? 0 : _amount,
					PaymentStatus = _paymentType == PaymentType.payment ? "preauth" : _paymentType == PaymentType.paymentAndCapture ? "captured" : "recurring_confirmed",
					Terminal = _terminal,
					CreditCardMaskedPan = "4000111122223333",
					CreditCardToken = "abcdefghijklmnopqrstuvxy"
				}
			};
		}
	}
}
