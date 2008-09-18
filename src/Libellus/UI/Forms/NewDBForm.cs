using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Libellus.Utilities;
using Libellus.DataAccess;

namespace Libellus.UI.Forms
{
    /// <summary>
    /// This form is for when the user clicks file->new->library
    /// </summary>
    public partial class NewDBForm : BaseForm
    {

 

        /// <summary>
        /// generated constructor
        /// </summary>
        public NewDBForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// is called when the checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkProtected_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkProtected.Checked == true)
            {
                this.lblPassword.Enabled = true;
                this.txtPassword.Enabled = true;
            }
            else
            {
                this.txtPassword.Text = "";
                this.lblPassword.Enabled = false;
                this.txtPassword.Enabled = false;
            }
        }

        /// <summary>
        /// is called when cancel is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// called when OK button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            int errors = 0;
            //check and make sure everything is filled out
            errors += UIUtils.ValidationHelper(this.txtOwner.Text.Length == 0,this.errorProvider1,this.txtOwner,ErrorMessages.Common.REQUIRED_FIELD);
            errors += UIUtils.ValidationHelper(this.txtLibrary.Text.Length == 0,this.errorProvider1,this.txtLibrary,ErrorMessages.Common.REQUIRED_FIELD);
            errors += UIUtils.ValidationHelper(this.chkProtected.Checked == true && this.txtPassword.Text.Length == 0,this.errorProvider1,this.txtPassword,ErrorMessages.NewDBForm.PASSWORD);

            if (errors > 0)
                return;

            //copy the empty database and rename it
            FileInfo emptyDB = new FileInfo("emdata");

            if(!emptyDB.Exists)
            {
                MessageBox.Show(emptyDB.FullName);
                MessageBox.Show(ErrorMessages.NewDBForm.EMPTY_DB_MISSING,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            

            string newFileName = this.txtLibrary.Text.Replace(" ","_") + ".plb";
            //check to make sure we're not writing over anything
            FileInfo newFile = new FileInfo(newFileName);

            if (newFile.Exists)
            {
                MessageBox.Show(ErrorMessages.NewDBForm.DB_ALREADY_EXISTS, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                newFile = emptyDB.CopyTo(newFileName);
            }
            catch (Exception e1)
            {
                ExceptionHandler.HandleException(e1);
                this.Close();
                return;
            }
            
            
            if (!newFile.Exists)
            {
                MessageBox.Show(ErrorMessages.NewDBForm.DB_NOT_CREATED, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if we reached here, we have a database, now initialize it
            BaseInfoDAO dao = new BaseInfoDAO(newFileName);
            bool success = dao.InitializeDatabase(this.txtOwner.Text, this.txtLibrary.Text, this.txtPassword.Text);
           
            if (!success)
            {
                MessageBox.Show(ErrorMessages.NewDBForm.DB_NOT_CREATED, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                newFile.Delete();
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show(Messages.NewDBForm.DB_CREATED, "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    MainForm form = (MainForm)this.MdiParent;
                    form.CurrentDatabase = newFile;

                    //check and see if there is a ViewDB window open, if so, close it
                    Form[] children = form.MdiChildren;
                    foreach (Form f in children)
                    {
                        if (f is ViewDatabasesForm)
                            f.Close();
                    }

                    ViewBooksForm f2 = new ViewBooksForm(Constants.LibraryMode.LIBRARY);
                    f2.MdiParent = this.MdiParent;
                    f2.Show();
                }
                else
                {
                    //check for a view database window, and refresh it
                    MainForm form = (MainForm)this.MdiParent;
                    Form[] children = form.MdiChildren;
                    foreach (Form f in children)
                    {
                        if (f is ViewDatabasesForm)
                            f.Refresh();
                    }
                }
            }
            this.Close();

           
        }


    }
}
