using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Libellus.Utilities;
using Libellus.Domain;
using Libellus.DataAccess;

namespace Libellus.UI.Forms
{
    public partial class ViewDatabasesForm : BaseForm
    {
        private FileInfo[] filesInfo;

        #region Constructor
        public ViewDatabasesForm()
        {
            InitializeComponent();
            this.grdDBList.CellDoubleClick += new DataGridViewCellEventHandler(grdDBList_CellDoubleClick);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Called when this method Show() is called
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            MainForm form = (MainForm)this.MdiParent;
            form.SetStatusStripText("Viewing all available libraries");

            this.refreshGrid();
        }

        /// <summary>
        /// Called when the "open" button is clicked on this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            FileInfo currentDB = this.getSelectedDatabase();
            if (currentDB == null)
                return;

            MainForm parent = this.MdiParent as MainForm;


            BaseInfoDAO dao = new BaseInfoDAO(currentDB.FullName);
            BaseInfo baseInfo = dao.GetBaseInfo();
            if (!baseInfo.Password.Equals(""))
            {
                PasswordPrompt prompt = new PasswordPrompt(baseInfo.Password);
                if (prompt.ShowDialog() != DialogResult.OK)
                    return;
            }   

            MainForm form = (MainForm)this.MdiParent;
            form.CurrentDatabase = currentDB;
            this.Close();

            foreach (Form f in parent.MdiChildren)
                f.Close();

            ViewBooksForm booksForm = new ViewBooksForm(Constants.LibraryMode.LIBRARY);
            booksForm.MdiParent = form;
            booksForm.Show();

        }

        private void grdDBList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
			MainForm parent = this.MdiParent as MainForm;
            DataGridViewCell cell = this.grdDBList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell is DataGridViewCheckBoxCell)
            {
                DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)cell;
                string val = (string)chkCell.Value;
                if (val != null && val.Equals("false"))
                {
                    //save the setting
                    parent.AppSettings.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value = this.filesInfo[e.RowIndex].Name;
                    parent.AppSettings.Save(System.Configuration.ConfigurationSaveMode.Modified);

                    //clear all all other default databases
                    for (int i = 0; i < this.grdDBList.Rows.Count; i++)
                    {
                        if (i != e.RowIndex)
                        {
                            DataGridViewCheckBoxCell otherCell = (DataGridViewCheckBoxCell)this.grdDBList.Rows[i].Cells["Default"];
                            otherCell.Value = "false";
                        }
                    }
                }
                else
                {
                	parent.AppSettings.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value = "";
                	parent.AppSettings.Save(System.Configuration.ConfigurationSaveMode.Modified);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            FileInfo currentDB = this.getSelectedDatabase();
            //make sure there isn't a password in this database
            BaseInfoDAO dao = new BaseInfoDAO(currentDB.FullName);
            BaseInfo baseInfo = dao.GetBaseInfo();
            if (!baseInfo.Password.Equals(""))
            {
                PasswordPrompt prompt = new PasswordPrompt(baseInfo.Password);
                if (prompt.ShowDialog() != DialogResult.OK)
                    return;
            }
            if (MessageBox.Show(Messages.ViewDBForm.DB_DELETE_CONFIRM, "Delete This Library?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                currentDB.Delete();
                if (currentDB.Exists)
                    MessageBox.Show(ErrorMessages.Common.COULD_NOT_DELETE_DB, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(Messages.Common.DB_DELETED, "Library Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainForm form = (MainForm)this.MdiParent;
                form.CurrentDatabase = null;
            }
            this.refreshGrid();
        }

        /// <summary>
        /// Called when the "New Library" button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewDBForm form = new NewDBForm();
            form.MdiParent = this.MdiParent;
            form.Show();     
        }

        void grdDBList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.grdDBList.SelectedRows.Count == 1)
            {
                this.btnOpen_Click(this, null);
            }
        }
        #endregion

        #region Private Utilities Methods
        private FileInfo getSelectedDatabase()
        {
            int selected = -1;
            if (this.filesInfo != null && this.filesInfo.Length > 0)
            {
                for (int i = 0; i < this.filesInfo.Length; i++)
                {
                    if (this.grdDBList.Rows[i].Selected == true)
                    {
                        selected = i;
                    }
                }
            }

            if (selected == -1)
            {
                MessageBox.Show(ErrorMessages.ViewDBForm.DB_NOT_SELECTED, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            else
            {
                return this.filesInfo[selected];
            }
        }

        private void refreshGrid()
        {
        	MainForm parent = this.MdiParent as MainForm;
            //look in the datadirectory, get a list of its contents
            this.grdDBList.Rows.Clear();
            
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory());
            this.filesInfo = info.GetFiles("*.plb");

            //iterate over all files in the directory
            for (int i = 0; i < this.filesInfo.Length; i++)
            {
                BaseInfoDAO dao = new BaseInfoDAO(this.filesInfo[i].FullName);
                BaseInfo baseInfo = dao.GetBaseInfo();
                if (baseInfo != null)
                {
                    int index = this.grdDBList.Rows.Add();
                    DataGridViewRow row = this.grdDBList.Rows[index];
                    row.Cells[0].Value = baseInfo.DBName;
                    row.Cells[1].Value = baseInfo.Owner;
                    row.Cells[2].Value = baseInfo.DateCreated;
                    row.Cells[3].Value = baseInfo.LastAccessed;
                    string s1 = parent.AppSettings.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value;
                    string s2 = this.filesInfo[i].Name;
                    if (parent.AppSettings.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value == this.filesInfo[i].Name)
                        row.Cells[4].Value = "true";
                    else
                        row.Cells[4].Value = "false";
                }
            }

            //select the first row
            if (this.grdDBList.Rows.Count > 0)
                this.grdDBList.Rows[0].Selected = true;
        }
        #endregion

        #region Overridden Methods
        public override void Refresh()
        {
            base.Refresh();
            this.refreshGrid();
        }
        #endregion


    }
}
