using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AltaPay.Service;

namespace AltaPay.Moto
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

		public void Initialize(IMotoController controller, string orderId, double amount, AuthType paymentType)
		{
			OrderId.Text = orderId;
			Amount.Text = amount.ToString();
			PaymentType.Text = paymentType.ToString();
			_controller = controller;
		}

		public void AddCreditCard(string maskedPan, string cardToken)
		{
			_existingPans.Add(new ExistingCreditCard { CardToken=cardToken, MaskedPan=maskedPan });
		}

		public void SetAvsInfo(
			string firstName
			, string lastName
			, string address
			, string postalCode
			, string city
			, string region
			, string country
			, string phone
			, string email)
		{
			FirstName.Text = firstName;
			LastName.Text = lastName;
			Address.Text = address;
			PostalCode.Text = postalCode;
			City.Text = city;
			Region.Text = region;
			Country.Text = country;
			Phone.Text = phone;
			Email.Text = email;
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
			_controller.PayUsingExistingCreditCard(ExistingPans.SelectedValue.ToString(), existingCvc.Text, getAvsInfo());
		}

		private void SubmitNewCard_Click(object sender, EventArgs e)
		{
			DisableView();
			_controller.PayUsingNewCreditCard(newPan.Text, int.Parse(ExpiryMonth.Text), int.Parse(ExpiryYear.Text), newCvc.Text, getAvsInfo());
		}

		private AvsInfo getAvsInfo()
		{
			return new AvsInfo
			{
				Address = Address.Text
				, City = City.Text
				, Country = Country.Text
				, Email = Email.Text
				, FirstName = FirstName.Text
				, LastName = LastName.Text
				, Phone = Phone.Text
				, PostalCode = PostalCode.Text
				, Region = Region.Text
			};
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

		private void tabPage2_Click(object sender, EventArgs e)
		{

		}

		private void newForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SubmitNewCard_Click(sender,e);
			}
		}

		private void existingForm_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SubmitExistingCard_Click(sender, e);
			}
		}
	}
}
