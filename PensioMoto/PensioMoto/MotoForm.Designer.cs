namespace PensioMoto
{
    partial class MotoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.ExistingPans = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.newPan = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ExpiryMonth = new System.Windows.Forms.ComboBox();
			this.ExpiryYear = new System.Windows.Forms.ComboBox();
			this.existingCvc = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.newCvc = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SubmitExistingCard = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.SubmitNewCard = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.PaymentType = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.Amount = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.OrderId = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.Cancel = new System.Windows.Forms.Button();
			this.Status = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.Phone = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.Email = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.FirstName = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.LastName = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.Address = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.Country = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.Region = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.PostalCode = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.City = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ExistingPans
			// 
			this.ExistingPans.FormattingEnabled = true;
			this.ExistingPans.Location = new System.Drawing.Point(93, 16);
			this.ExistingPans.Name = "ExistingPans";
			this.ExistingPans.Size = new System.Drawing.Size(195, 108);
			this.ExistingPans.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Existing cards";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "New card pan";
			// 
			// newPan
			// 
			this.newPan.Location = new System.Drawing.Point(84, 13);
			this.newPan.Name = "newPan";
			this.newPan.Size = new System.Drawing.Size(133, 20);
			this.newPan.TabIndex = 1;
			this.newPan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newForm_KeyUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Expiry month";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(58, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Expiry year";
			// 
			// ExpiryMonth
			// 
			this.ExpiryMonth.FormattingEnabled = true;
			this.ExpiryMonth.Location = new System.Drawing.Point(84, 39);
			this.ExpiryMonth.Name = "ExpiryMonth";
			this.ExpiryMonth.Size = new System.Drawing.Size(133, 21);
			this.ExpiryMonth.TabIndex = 2;
			this.ExpiryMonth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newForm_KeyUp);
			// 
			// ExpiryYear
			// 
			this.ExpiryYear.FormattingEnabled = true;
			this.ExpiryYear.Location = new System.Drawing.Point(84, 66);
			this.ExpiryYear.Name = "ExpiryYear";
			this.ExpiryYear.Size = new System.Drawing.Size(133, 21);
			this.ExpiryYear.TabIndex = 3;
			this.ExpiryYear.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newForm_KeyUp);
			// 
			// existingCvc
			// 
			this.existingCvc.Location = new System.Drawing.Point(93, 130);
			this.existingCvc.Name = "existingCvc";
			this.existingCvc.Size = new System.Drawing.Size(195, 20);
			this.existingCvc.TabIndex = 6;
			this.existingCvc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.existingForm_KeyUp);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 133);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(28, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "CVC";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(28, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "CVC";
			// 
			// newCvc
			// 
			this.newCvc.Location = new System.Drawing.Point(84, 93);
			this.newCvc.Name = "newCvc";
			this.newCvc.Size = new System.Drawing.Size(133, 20);
			this.newCvc.TabIndex = 8;
			this.newCvc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.newForm_KeyUp);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.ExistingPans);
			this.groupBox1.Controls.Add(this.SubmitExistingCard);
			this.groupBox1.Controls.Add(this.existingCvc);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(313, 188);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Existing Cards";
			// 
			// SubmitExistingCard
			// 
			this.SubmitExistingCard.Location = new System.Drawing.Point(213, 157);
			this.SubmitExistingCard.Name = "SubmitExistingCard";
			this.SubmitExistingCard.Size = new System.Drawing.Size(75, 23);
			this.SubmitExistingCard.TabIndex = 13;
			this.SubmitExistingCard.Text = "Submit";
			this.SubmitExistingCard.UseVisualStyleBackColor = true;
			this.SubmitExistingCard.Click += new System.EventHandler(this.SubmitExistingCard_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.newPan);
			this.groupBox2.Controls.Add(this.SubmitNewCard);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.newCvc);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.ExpiryYear);
			this.groupBox2.Controls.Add(this.ExpiryMonth);
			this.groupBox2.Location = new System.Drawing.Point(325, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(229, 188);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "New card";
			// 
			// SubmitNewCard
			// 
			this.SubmitNewCard.Location = new System.Drawing.Point(142, 157);
			this.SubmitNewCard.Name = "SubmitNewCard";
			this.SubmitNewCard.Size = new System.Drawing.Size(75, 23);
			this.SubmitNewCard.TabIndex = 13;
			this.SubmitNewCard.Text = "Submit";
			this.SubmitNewCard.UseVisualStyleBackColor = true;
			this.SubmitNewCard.Click += new System.EventHandler(this.SubmitNewCard_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.PaymentType);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.Amount);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.OrderId);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(548, 46);
			this.groupBox3.TabIndex = 12;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Payment info";
			// 
			// PaymentType
			// 
			this.PaymentType.Enabled = false;
			this.PaymentType.Location = new System.Drawing.Point(403, 13);
			this.PaymentType.Name = "PaymentType";
			this.PaymentType.Size = new System.Drawing.Size(100, 20);
			this.PaymentType.TabIndex = 5;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(325, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(74, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Payment type:";
			// 
			// Amount
			// 
			this.Amount.Enabled = false;
			this.Amount.Location = new System.Drawing.Point(218, 13);
			this.Amount.Name = "Amount";
			this.Amount.Size = new System.Drawing.Size(100, 20);
			this.Amount.TabIndex = 3;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(165, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(46, 13);
			this.label8.TabIndex = 2;
			this.label8.Text = "Amount:";
			// 
			// OrderId
			// 
			this.OrderId.Enabled = false;
			this.OrderId.Location = new System.Drawing.Point(59, 13);
			this.OrderId.Name = "OrderId";
			this.OrderId.Size = new System.Drawing.Size(100, 20);
			this.OrderId.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 13);
			this.label7.TabIndex = 0;
			this.label7.Text = "Order id:";
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(485, 301);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 14;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// Status
			// 
			this.Status.Location = new System.Drawing.Point(12, 303);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(467, 20);
			this.Status.TabIndex = 15;
			this.Status.Text = "Awaiting user input.....";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 64);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(571, 231);
			this.tabControl1.TabIndex = 16;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(563, 205);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Creditcard Info";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.Phone);
			this.tabPage2.Controls.Add(this.label18);
			this.tabPage2.Controls.Add(this.Email);
			this.tabPage2.Controls.Add(this.label17);
			this.tabPage2.Controls.Add(this.FirstName);
			this.tabPage2.Controls.Add(this.label16);
			this.tabPage2.Controls.Add(this.LastName);
			this.tabPage2.Controls.Add(this.label15);
			this.tabPage2.Controls.Add(this.Address);
			this.tabPage2.Controls.Add(this.label14);
			this.tabPage2.Controls.Add(this.Country);
			this.tabPage2.Controls.Add(this.label13);
			this.tabPage2.Controls.Add(this.Region);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.PostalCode);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.City);
			this.tabPage2.Controls.Add(this.label10);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(563, 205);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Address Verification Info";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
			// 
			// Phone
			// 
			this.Phone.Location = new System.Drawing.Point(335, 32);
			this.Phone.Name = "Phone";
			this.Phone.Size = new System.Drawing.Size(128, 20);
			this.Phone.TabIndex = 17;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(264, 35);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(38, 13);
			this.label18.TabIndex = 16;
			this.label18.Text = "Phone";
			// 
			// Email
			// 
			this.Email.Location = new System.Drawing.Point(335, 6);
			this.Email.Name = "Email";
			this.Email.Size = new System.Drawing.Size(128, 20);
			this.Email.TabIndex = 15;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(264, 9);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(32, 13);
			this.label17.TabIndex = 14;
			this.label17.Text = "Email";
			// 
			// FirstName
			// 
			this.FirstName.Location = new System.Drawing.Point(79, 6);
			this.FirstName.Name = "FirstName";
			this.FirstName.Size = new System.Drawing.Size(128, 20);
			this.FirstName.TabIndex = 13;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(8, 9);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(55, 13);
			this.label16.TabIndex = 12;
			this.label16.Text = "First name";
			// 
			// LastName
			// 
			this.LastName.Location = new System.Drawing.Point(79, 32);
			this.LastName.Name = "LastName";
			this.LastName.Size = new System.Drawing.Size(128, 20);
			this.LastName.TabIndex = 11;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(8, 35);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(56, 13);
			this.label15.TabIndex = 10;
			this.label15.Text = "Last name";
			// 
			// Address
			// 
			this.Address.Location = new System.Drawing.Point(79, 58);
			this.Address.Name = "Address";
			this.Address.Size = new System.Drawing.Size(128, 20);
			this.Address.TabIndex = 9;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(8, 61);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(45, 13);
			this.label14.TabIndex = 8;
			this.label14.Text = "Address";
			// 
			// Country
			// 
			this.Country.Location = new System.Drawing.Point(79, 162);
			this.Country.Name = "Country";
			this.Country.Size = new System.Drawing.Size(128, 20);
			this.Country.TabIndex = 7;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 165);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(43, 13);
			this.label13.TabIndex = 6;
			this.label13.Text = "Country";
			// 
			// Region
			// 
			this.Region.Location = new System.Drawing.Point(79, 136);
			this.Region.Name = "Region";
			this.Region.Size = new System.Drawing.Size(128, 20);
			this.Region.TabIndex = 5;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(8, 139);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 13);
			this.label12.TabIndex = 4;
			this.label12.Text = "Region";
			// 
			// PostalCode
			// 
			this.PostalCode.Location = new System.Drawing.Point(79, 110);
			this.PostalCode.Name = "PostalCode";
			this.PostalCode.Size = new System.Drawing.Size(128, 20);
			this.PostalCode.TabIndex = 3;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 113);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(64, 13);
			this.label11.TabIndex = 2;
			this.label11.Text = "Postal Code";
			// 
			// City
			// 
			this.City.Location = new System.Drawing.Point(79, 84);
			this.City.Name = "City";
			this.City.Size = new System.Drawing.Size(128, 20);
			this.City.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 87);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "City";
			// 
			// MotoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(788, 503);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.groupBox3);
			this.Name = "MotoForm";
			this.Text = "MotoForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ExistingPans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newPan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ExpiryMonth;
        private System.Windows.Forms.ComboBox ExpiryYear;
        private System.Windows.Forms.TextBox existingCvc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox newCvc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox OrderId;
        private System.Windows.Forms.TextBox Amount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox PaymentType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button SubmitExistingCard;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox Status;
		private System.Windows.Forms.Button SubmitNewCard;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox Country;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox Region;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox PostalCode;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox City;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox Address;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox Email;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox FirstName;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox LastName;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox Phone;
		private System.Windows.Forms.Label label18;
    }
}