using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Libellus.Utilities;
using Libellus.Domain;
using Libellus.UI.Controls;
using Libellus.DataAccess;

namespace Libellus.UI.Forms
{
    public partial class ViewBooksForm : Libellus.UI.Forms.BaseForm
    {
        #region Private Data
        private Constants.LibraryMode _libMode;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="libMode"></param>
        /// <param name="bookId"></param>
        public ViewBooksForm(Constants.LibraryMode libMode, string bookId)
        {
            _libMode = libMode;
            InitializeComponent();
            this.bookGrid1.LibraryMode = _libMode;
            this.bookGrid1.BookId = bookId;
            this.bookSearch1.BookGrid = this.bookGrid1;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="libMode"></param>
        public ViewBooksForm(Constants.LibraryMode libMode)
        {
            _libMode = libMode;
            InitializeComponent();
            this.bookGrid1.LibraryMode = _libMode;
            this.bookGrid1.BookId = null;
            this.bookSearch1.BookGrid = this.bookGrid1;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gives access to the forms mode
        /// </summary>
        public Constants.LibraryMode LibraryMode
        {
            get { return _libMode; }
        }

        /// <summary>
        /// gives public access to the underlying data grid
        /// </summary>
        public BookGrid Grid
        {
            get { return this.bookGrid1; }
        }
        #endregion

        #region Private Utility Methods
        /// <summary>
        /// Sets the title bar text based on the mode the current window is in
        /// </summary>
        /// <param name="libMode"></param>
        public void SetTitleBar(Constants.LibraryMode libMode, string optionalValue)
        {
            MainForm parent = this.MdiParent as MainForm;
            BaseInfo info = (new BaseInfoDAO(parent.CurrentDatabase.FullName)).GetBaseInfo();
            switch (libMode)
            {
                case Constants.LibraryMode.LIBRARY:
                    this.Text = "Viewing " + Utils.GetPossessive(info.Owner) + " Library";
                    break;

                case Constants.LibraryMode.LOANEDBOOKS:
                    this.Text = "Viewing " + Utils.GetPossessive(info.Owner) + " Loaned Out Books";
                    break;

                case Constants.LibraryMode.WISHLIST:
                    this.Text = "Viewing " + Utils.GetPossessive(info.Owner) + " Wishlist";
                    break;

                case Constants.LibraryMode.LOANHISTORY:
                    this.Text = "Viewing Loan History of " + optionalValue;
                    break;
            }
        }

        #endregion

        #region Overridden Methods
        /// <summary>
        /// Is called when the forms show() method is called
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.SetTitleBar(_libMode,"");
            this.bookGrid1.Refresh();
        }

        /// <summary>
        /// Overrides refresh options
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            this.bookGrid1.Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            try
            {
                this.bookGrid1.Width = this.Size.Width - 35;
                this.bookGrid1.Height = this.Size.Height - this.bookSearch1.Size.Height - 60;
            }
            catch (Exception e1)
            {
            	Log.Instance.Out(e1);
            }
        }

        public DataGridView DataGrid
        {
            get { return this.bookGrid1.DataGrid; }
        }
        
        public void FillViewMenu(ToolStripMenuItem menu)
        {
        	foreach(DataGridViewColumn column in this.Grid.DataGrid.Columns)
        	{
        		if(column.HeaderText == "Id")
        			continue;
        		
        		if(_libMode != Constants.LibraryMode.LOANEDBOOKS && _libMode != Constants.LibraryMode.LOANHISTORY)
        		{
        			if(column.HeaderText == "Loaned To" || column.HeaderText == "Loan Date" || column.HeaderText == "Checked In")
        				continue;
        		}
        		ToolStripMenuItem item = new ToolStripMenuItem();
        		item.Text = column.HeaderText;
        		item.Name = column.Name;
        		item.Checked = column.Visible;
        		item.CheckOnClick = true;
        		item.CheckedChanged += new EventHandler(ViewMenuItem_Checked);
        		menu.DropDownItems.Add(item);
        	}
        }
        
        public void ViewMenuItem_Checked(object sender, System.EventArgs e)
        {
        	ToolStripMenuItem item = sender as ToolStripMenuItem;
        	DataGridViewColumn column = this.Grid.DataGrid.Columns[item.Name];
       		column.Visible = item.Checked;
        }

        #endregion

        
    }
}

