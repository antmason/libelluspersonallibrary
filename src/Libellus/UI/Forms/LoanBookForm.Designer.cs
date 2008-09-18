namespace Libellus.UI.Forms
{
    partial class LoanBookForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtLoanTo = new System.Windows.Forms.TextBox();
            this.lblBook = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLoanTo = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtLoanTo);
            this.groupBox1.Controls.Add(this.lblBook);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblLoanTo);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 147);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loan Book";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtLoanTo
            // 
            this.txtLoanTo.Location = new System.Drawing.Point(69, 60);
            this.txtLoanTo.Name = "txtLoanTo";
            this.txtLoanTo.Size = new System.Drawing.Size(202, 20);
            this.txtLoanTo.TabIndex = 1;
            // 
            // lblBook
            // 
            this.lblBook.AutoEllipsis = true;
            this.lblBook.Location = new System.Drawing.Point(66, 25);
            this.lblBook.MaximumSize = new System.Drawing.Size(250, 250);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(205, 32);
            this.lblBook.TabIndex = 13;
            this.lblBook.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Book: ";
            // 
            // lblLoanTo
            // 
            this.lblLoanTo.AutoSize = true;
            this.lblLoanTo.Location = new System.Drawing.Point(6, 63);
            this.lblLoanTo.Name = "lblLoanTo";
            this.lblLoanTo.Size = new System.Drawing.Size(50, 13);
            this.lblLoanTo.TabIndex = 11;
            this.lblLoanTo.Text = "Loan To:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(69, 96);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LoanBookForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(326, 172);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(334, 206);
            this.MinimumSize = new System.Drawing.Size(334, 206);
            this.Name = "LoanBookForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLoanTo;
        private System.Windows.Forms.Label lblBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLoanTo;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ErrorProvider errorProvider1;

    }
}
