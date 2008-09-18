using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Libellus.Utilities;

namespace Libellus.UI.Forms
{
    public partial class PasswordPrompt : Form
    {
        private string _correctPW;
        public PasswordPrompt(string password)
        {
            InitializeComponent();
            this._correctPW = password;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text.Equals(this._correctPW))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(ErrorMessages.Common.INCORRECT_PASSWORD, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
