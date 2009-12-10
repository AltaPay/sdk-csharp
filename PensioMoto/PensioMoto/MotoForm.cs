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
		private List<ExistingCreditCard> _existingPans;

		private IMotoController _controller;

        public MotoForm()
        {
            InitializeComponent();

			_existingPans = new List<ExistingCreditCard>();
			
            ExpiryMonth.DataSource = new List<int>() { 1,2,3,4,5,6,7,8,9,10,11,12 };
            ExpiryYear.DataSource = new List<int>() { 2009,2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020 };
        }

		public void Initialize(IMotoController controller, string orderId, double amount, PaymentType paymentType)
		{
			OrderId.Text = orderId;
			Amount.Text = amount.ToString();
			PaymentType.Text = paymentType.ToString();
			_controller = controller;
		}

		public void SetCreditCard(string maskedPan, string cardToken)
		{
			_existingPans.Add(new ExistingCreditCard { CardToken=cardToken, MaskedPan=maskedPan });
		}

		public void ShowBlocking()
		{
			ShowDialog();
		}

		public void DisableView()
		{
			ExistingPans.Enabled = false;
			existingCvc.Enabled = false;
			newPan.Enabled = false;
			ExpiryMonth.Enabled = false;
			ExpiryYear.Enabled = false;
			newCvc.Enabled = false;
			SubmitExistingCard.Enabled = false;
			SubmitNewCard.Enabled = false;
		}

		public void EnableView(string status)
		{
			ExistingPans.Enabled = true;
			existingCvc.Enabled = true;
			newPan.Enabled = true;
			ExpiryMonth.Enabled = true;
			ExpiryYear.Enabled = true;
			newCvc.Enabled = true;
			SubmitExistingCard.Enabled = true;
			SubmitNewCard.Enabled = true;

			Status.Text = status;
		}

		protected override void OnShown(EventArgs e)
		{
			ExistingPans.DisplayMember = "MaskedPan";
			ExistingPans.ValueMember = "CardToken";
			ExistingPans.DataSource = _existingPans;
			base.OnShown(e);
		}

		private void SubmitExistingCard_Click(object sender, EventArgs e)
		{
			DisableView();
			_controller.PayUsingExistingCreditCard(ExistingPans.SelectedValue.ToString(), int.Parse(existingCvc.Text));
		}

		private void SubmitNewCard_Click(object sender, EventArgs e)
		{
			DisableView();
			_controller.PayUsingNewCreditCard(newPan.Text, int.Parse(ExpiryMonth.Text), int.Parse(ExpiryYear.Text), int.Parse(newCvc.Text));
		}

		protected class ExistingCreditCard
		{
			public string CardToken { get; set; }
			public string MaskedPan { get; set; }
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
			_controller.Cancel();
		}
	}
}
