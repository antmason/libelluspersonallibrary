using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Libellus.DataAccess;
using Libellus.Utilities;

namespace Libellus.UI.Forms
{
    public partial class LoanBookForm : Libellus.UI.Forms.BaseForm
    {
        private string _bookId;
        private MainForm _topParent;

        public LoanBookForm(string bookId,string bookTitle,MainForm topParent)
        {
            InitializeComponent();
            _bookId = bookId;
            _topParent = topParent;
            this.lblBook.Text = bookTitle;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!this.checkRequiredFields())
                return;
            
            BookDAO dao = new BookDAO(_topParent.CurrentDatabase.FullName);

            bool success = dao.LoanBook(_bookId, this.txtLoanTo.Text);
            if (!success)
            {
                MessageBox.Show("An unknown error occurred and this book was not updated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                _topParent.SetStatusStripText("Book " + this.lblBook.Text + " was successfully loaned to " + this.txtLoanTo.Text);
                this.Close();
            }
        }

        private bool checkRequiredFields()
        {
            if (this.txtLoanTo.Text.Equals(""))
            {
                this.errorProvider1.SetError(this.txtLoanTo, ErrorMessages.Common.REQUIRED_FIELD);
                return false;
            }
            else
            {
                this.errorProvider1.SetError(this.txtLoanTo, "");
                return true;
            }
        }
    }
}

