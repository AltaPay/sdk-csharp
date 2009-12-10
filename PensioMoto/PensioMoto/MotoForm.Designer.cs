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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.PaymentType = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.Amount = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.OrderId = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.Submit = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.Status = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
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
			// 
			// ExpiryYear
			// 
			this.ExpiryYear.FormattingEnabled = true;
			this.ExpiryYear.Location = new System.Drawing.Point(84, 66);
			this.ExpiryYear.Name = "ExpiryYear";
			this.ExpiryYear.Size = new System.Drawing.Size(133, 21);
			this.ExpiryYear.TabIndex = 3;
			// 
			// existingCvc
			// 
			this.existingCvc.Location = new System.Drawing.Point(93, 130);
			this.existingCvc.Name = "existingCvc";
			this.existingCvc.Size = new System.Drawing.Size(195, 20);
			this.existingCvc.TabIndex = 6;
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
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.ExistingPans);
			this.groupBox1.Controls.Add(this.existingCvc);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(12, 81);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(313, 180);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Existing Cards";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.newPan);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.newCvc);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.ExpiryYear);
			this.groupBox2.Controls.Add(this.ExpiryMonth);
			this.groupBox2.Location = new System.Drawing.Point(331, 81);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(229, 180);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "New card";
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
			this.groupBox3.Size = new System.Drawing.Size(548, 63);
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
			// Submit
			// 
			this.Submit.Location = new System.Drawing.Point(485, 267);
			this.Submit.Name = "Submit";
			this.Submit.Size = new System.Drawing.Size(75, 23);
			this.Submit.TabIndex = 13;
			this.Submit.Text = "Submit";
			this.Submit.UseVisualStyleBackColor = true;
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(404, 267);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 14;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// Status
			// 
			this.Status.Location = new System.Drawing.Point(12, 267);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(383, 20);
			this.Status.TabIndex = 15;
			this.Status.Text = "Awaiting user input.....";
			// 
			// MotoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 307);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Submit);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "MotoForm";
			this.Text = "MotoForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox Status;
    }
}