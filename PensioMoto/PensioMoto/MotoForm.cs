using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PensioMoto.Service;

namespace PensioMoto
{
    public partial class MotoForm : Form, IMotoDialogView
    {
		private Dictionary<string, string> _existingPans;

        public MotoForm()
        {
            InitializeComponent();

			_existingPans = new Dictionary<string, string>();
            ExpiryMonth.DataSource = new List<int>() { 1,2,3,4,5,6,7,8,9,10,11,12 };
            ExpiryYear.DataSource = new List<int>() { 2009,2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020 };
        }

		public void Initialize(string orderId, double amount, PaymentType paymentType)
		{
			OrderId.Text = orderId;
			Amount.Text = amount.ToString();
			PaymentType.Text = paymentType.ToString();
		}

		public void SetCreditCard(string maskedPan, string cardToken)
		{
			_existingPans.Add(cardToken, maskedPan);
		}

		protected override void OnShown(EventArgs e)
		{
			ExistingPans.DataSource = _existingPans;
			base.OnShown(e);
		}
	}
}
