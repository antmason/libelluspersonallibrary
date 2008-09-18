using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Libellus.UI.Forms
{
    public partial class ExceptionWindow : Form
    {
        public ExceptionWindow(Exception e)
        {
            InitializeComponent();

            this.txtError.Text = e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string toEmail = "anthony.mason1@gmail.com";
            string subject = "Libellus: Exception Information";
            string notes = this.txtNotes.Text;
            string error = this.txtError.Text;
            string body = "Exception  occurred. \r\nNotes: \r\n" + notes + "\r\n" + error;
            body = body.Replace(" ", "%20");
            body = body.Replace("\r\n", "%0A");
            body = body.Replace("\t", "09");
            string message = string.Format("mailto:{0}?subject={1}&body={2}", toEmail,
            subject, body);

            Process.Start(message);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.antmason.com/forum/viewforum.php?f=1&sid=94a28abf474c80427d7b09cebf399c9c");
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string body = "Exception occurred.\r\n\r\nNotes:\r\n" + this.txtNotes.Text + "\r\n\r\n" + this.txtError.Text;
            Clipboard.SetText(body);
        }

    }
}
