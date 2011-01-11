using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PensioMoto.Service;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using PensioMoto.Service.Dto;

namespace PensioMoto
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public class Moto : IMoto
	{
		MotoDialog _dialog;

		public Moto()
		{
			_dialog = new MotoDialog(new MotoForm(), new MerchantApi());
		}

		public void Initialize(string gatewayUrl, string apiUsername, string apiPassword, string terminal, string orderId, double amount, int currency, string paymentType)
		{
			_dialog.Initialize(gatewayUrl, apiUsername, apiPassword, terminal, orderId, amount, currency, (PaymentType)Enum.Parse(typeof(PaymentType), paymentType));
		}

		public void AddCreditCard(string maskedPan, string cardToken)
		{
			_dialog.AddCreditCard(maskedPan, cardToken);
		}

		public void SetAvsInfo(string firstName, string lastName, string address, string postalCode, string city, string region, string country, string phone, string email)
		{
			_dialog.SetAvsInfo(firstName, lastName, address, postalCode, city, region, country, phone, email);
		}

		public IComPaymentResult Show()
		{
			return new ComPaymentResult(_dialog.Show());
		}
	}
}

