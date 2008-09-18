using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Libellus.Utilities;
using Libellus.UI.Forms;
using Libellus.DataAccess;
using Libellus.Domain;
using Libellus.Printing;

namespace Libellus.UI.Controls
{
    public partial class BookGrid : UserControl
    {
        #region Private Data
        private Constants.LibraryMode _mode;
        private string _bookId;
        private List<int> _printableColumns = new List<int>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BookGrid()
        {
            InitializeComponent();
            this.gridBooks.SelectionChanged += new EventHandler(gridBooks_SelectionChanged);
            this.gridBooks.CellDoubleClick += new DataGridViewCellEventHandler(gridBooks_CellDoubleClick);
        }
        #endregion

        #region Event Handlers

        void gridBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_mode == Constants.LibraryMode.LOANEDBOOKS)
            {
                this.viewLoanHistoryToolStripMenuItem_Click(this, null);
            }
            else
            {
                this.viewDetailsToolStripMenuItem_Click(this, null);
            }
        }

        void gridBooks_SelectionChanged(object sender, EventArgs e)
        {
            MainForm f = this.ParentForm.MdiParent as MainForm;
            if (this.gridBooks.SelectedRows.Count > 0)
            {
                switch (_mode)
                {
                    case Constants.LibraryMode.LIBRARY:
                        f.SetLibraryOperations(true, true, true);
                        this.transferBookToWishListToolStripMenuItem.Visible = true;
                        this.transferBookToWishListToolStripMenuItem.Enabled = true;
                        break;

                    case Constants.LibraryMode.AVAILABLEBOOKS:
                        f.SetLibraryOperations(true, false, true);
                        break;

                    case Constants.LibraryMode.WISHLIST:
                        f.SetWishListOperations(true, true);
                        this.transferBookToLibraryToolStripMenuItem.Visible = true;
                        this.transferBookToLibraryToolStripMenuItem.Enabled = true;
                        break;

                    case Constants.LibraryMode.LOANEDBOOKS:
                        f.SetLibraryOperations(false, true, true);
                        break;

                    default:
                        f.SetLibraryOperations(false, false, false);
                        f.SetWishListOperations(false, false);
                        break;
                }
            }
            else
            {
                f.SetLibraryOperations(false, false, false);
                f.SetWishListOperations(false, false);
                this.loanBookToolStripMenuItem.Enabled = false;
                this.deleteBookToolStripMenuItem.Enabled = false;
                this.checkDetailsToolStripMenuItem.Enabled = false;
                this.viewDetailsToolStripMenuItem.Enabled = false;
                this.viewLoanHistoryToolStripMenuItem.Enabled = false;
                this.transferBookToWishListToolStripMenuItem.Enabled = false;
                this.transferBookToLibraryToolStripMenuItem.Enabled = false;
            }

        }

        /// <summary>
        /// called when right click->Delete book is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete these books?", "Delete?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MainForm parent = (MainForm)this.ParentForm.MdiParent;
                BookDAO dao = new BookDAO(parent.CurrentDatabase.FullName);
                foreach (string id in this.SelectedIds)
                    dao.DeleteBook(id);

                this.Refresh();
            }
        }

        /// <summary>
        /// Called when right click ->view details is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm parent = (MainForm)this.ParentForm.MdiParent;
            BookDAO dao = new BookDAO(parent.CurrentDatabase.FullName);

            foreach (string id in this.SelectedIds)
            {
                AddBookForm form = new AddBookForm(_mode, Constants.AddBookMode.VIEWONLY);
                form.BookData = dao.GetBookById(id);
                form.MdiParent = parent;
                form.Show();
            }

        }

        /// <summary>
        /// Called when right click ->loan book is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loanBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm parent = this.ParentForm.MdiParent as MainForm;
            BookDAO dao = new BookDAO(parent.CurrentDatabase.FullName);


            foreach (DataGridViewRow row in this.gridBooks.SelectedRows)
            {
                string id = (row.Cells["id"].Value.ToString());
                string title = (row.Cells["short_title"].Value.ToString());


                if (dao.IsBookLoaned(id))
                {
                    MessageBox.Show("'" + title + "' has already been loaned, please check it in to loan it to someone different.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                LoanBookForm form = new LoanBookForm(id, title, parent);
                form.ShowDialog(this.ParentForm);
            }

            this.Refresh();

        }

        /// <summary>
        /// Called when right click->check in is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm form = this.ParentForm.MdiParent as MainForm;
            BookDAO dao = new BookDAO(form.CurrentDatabase.FullName);
            foreach (string id in SelectedIds)
            {
                dao.ReturnBook(id);
            }

            this.Refresh();
        }

        /// <summary>
        /// Called when right click -> view loan history is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewLoanHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (string id in this.SelectedIds)
            {
                ViewBooksForm form = new ViewBooksForm(Constants.LibraryMode.LOANHISTORY, id);
                form.MdiParent = this.ParentForm.MdiParent;
                form.Show();
            }
        }

        /// <summary>
        /// Called on right click inside the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int count = this.gridBooks.SelectedRows.Count;
            if (count < 1)
            {
                this.deleteBookToolStripMenuItem.Enabled = false;
                this.loanBookToolStripMenuItem.Enabled = false;
                this.checkDetailsToolStripMenuItem.Enabled = false;
                this.viewDetailsToolStripMenuItem.Enabled = false;
                this.viewLoanHistoryToolStripMenuItem.Enabled = false;
                this.transferBookToLibraryToolStripMenuItem.Enabled = false;
                this.transferBookToLibraryToolStripMenuItem.Visible = false;
                this.editBookToolStripMenuItem.Enabled = false;
                return;
            }
            else
            {
                if (_mode == Constants.LibraryMode.WISHLIST)
                {
                    this.transferBookToLibraryToolStripMenuItem.Enabled = true;
                    this.transferBookToLibraryToolStripMenuItem.Visible = true;
                    this.viewDetailsToolStripMenuItem.Enabled = true;
                    this.deleteBookToolStripMenuItem.Enabled = true;
                    return;
                }
                this.viewLoanHistoryToolStripMenuItem.Enabled = true;
                this.viewDetailsToolStripMenuItem.Enabled = true;
                this.deleteBookToolStripMenuItem.Enabled = true;
                this.editBookToolStripMenuItem.Enabled = true;
            }

            foreach (DataGridViewRow row in this.gridBooks.SelectedRows)
            {
                string checkedIn = row.Cells["date_checkedin"].Value.ToString();
                string loaned = row.Cells["date_loaned"].Value.ToString();

                if (loaned.Equals(""))
                {
                    this.checkDetailsToolStripMenuItem.Visible = false;
                    this.loanBookToolStripMenuItem.Visible = true;
                    this.loanBookToolStripMenuItem.Enabled = true;
                }
                else if (checkedIn.Equals(""))
                {
                    this.checkDetailsToolStripMenuItem.Visible = true;
                    this.checkDetailsToolStripMenuItem.Enabled = true;
                    this.loanBookToolStripMenuItem.Visible = false;
                }
                else
                {
                    this.checkDetailsToolStripMenuItem.Visible = false;
                    this.loanBookToolStripMenuItem.Visible = true;
                    this.loanBookToolStripMenuItem.Enabled = true;
                }
            }

        }

        private void transferBookToLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm form = this.ParentForm.MdiParent as MainForm;
            BookDAO dao = new BookDAO(form.CurrentDatabase.FullName);
            foreach (string id in this.SelectedIds)
            {
                dao.TransferFromWishListToLibrary(id);
            }

            this.Refresh();
        }

        private void transferBookToWishListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm form = this.ParentForm.MdiParent as MainForm;
            BookDAO dao = new BookDAO(form.CurrentDatabase.FullName);
            foreach (string id in this.SelectedIds)
            {
                dao.TransferFromLibraryToWishList(id);
            }
            this.Refresh();
        }
        #endregion

        #region Properties
        /// <summary>
        /// This is set and called mainly when loan history is used so the search is accurate
        /// </summary>
        public string BookId
        {
            get { return _bookId; }
            set { _bookId = value; }
        }

        /// <summary>
        /// this returns a list of all the book_id's from the selected rows in the grid
        /// </summary>
        public List<string> SelectedIds
        {
            get
            {
                List<string> list = new List<string>();
                foreach (DataGridViewRow row in this.gridBooks.SelectedRows)
                {
                    list.Add(row.Cells["id"].Value.ToString());
                }
                return list;
            }
        }

        /// <summary>
        /// This returns the enum of which mode we are in (Library, WishList, etc)...
        /// </summary>
        public Constants.LibraryMode LibraryMode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                if (_mode == Constants.LibraryMode.LOANEDBOOKS)
                {
                    this.date_loaned.Visible = true;
                    this.LoanedTo.Visible = true;
                    this.date_added.Visible = false;
                    this.subject.Visible = false;
                    this.edition_info.Visible = false;
                    this.publisher_info.Visible = false;
                }
                else if (_mode == Constants.LibraryMode.LOANHISTORY)
                {
                    this.date_loaned.Visible = true;
                    this.LoanedTo.Visible = true;
                    this.date_added.Visible = false;
                    this.date_checkedin.Visible = true;
                    this.subject.Visible = false;
                    this.edition_info.Visible = false;
                    this.publisher_info.Visible = false;
                    this.gridBooks.Columns["short_title"].Visible = false;
                }
                else if (_mode == Constants.LibraryMode.WISHLIST)
                {
                    this.loanBookToolStripMenuItem.Visible = false;
                    this.checkDetailsToolStripMenuItem.Visible = false;
                    this.viewLoanHistoryToolStripMenuItem.Visible = false;
                }

            }
        }

        public List<Book> SelectedBooks
        {
            get
            {
                List<Book> list = new List<Book>();
                foreach (DataGridViewRow row in this.gridBooks.SelectedRows)
                {
                    Book b = new Book();
                    b.Id = row.Cells["id"].Value.ToString();
                    b.ShortTitle = row.Cells["short_title"].Value.ToString();
                    list.Add(b);
                }
                return list;
            }
        }
        #endregion

        #region Private Utility Functions

        #endregion

        #region Public Functions
        /// <summary>
        /// This calls on the dao to get the results depending on the mode we are currently in
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="value"></param>
        public void Search(Constants.SearchMode mode, string value)
        {
            MainForm main = (MainForm)this.ParentForm.MdiParent;
            BookDAO dao = new BookDAO(main.CurrentDatabase.FullName);
            this.gridBooks.DataSource = dao.SearchBooks(_mode, mode, value).Tables[0];
        }

        public DataGridView DataGrid
        {
            get { return this.gridBooks; }
        }

        #endregion

        #region Overridden Methods
        /// <summary>
        /// refreshes the grid with base results
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            MainForm parent = (MainForm)this.ParentForm.MdiParent;
            BookDAO dao = new BookDAO(parent.CurrentDatabase.FullName);
            if (_mode == Constants.LibraryMode.LOANHISTORY)
                this.gridBooks.DataSource = dao.SearchBooks(_mode, Constants.SearchMode.BOOK_ID, _bookId).Tables[0];
            else
                this.gridBooks.DataSource = dao.SearchBooks(_mode, Constants.SearchMode.NONE, null).Tables[0];

            if (_mode == Constants.LibraryMode.LOANHISTORY && this.gridBooks.Rows.Count > 0)
            {
                ViewBooksForm f = this.ParentForm as ViewBooksForm;
                f.SetTitleBar(_mode, this.gridBooks.Rows[0].Cells["short_title"].Value.ToString());
            }
        }

        private void toolStrip_OnClick(object sender, System.EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
        }

        protected override void OnResize(EventArgs e)
        {
            this.gridBooks.Height = this.Height;
            this.gridBooks.Width = this.Width;
        }
        #endregion

        private void addBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm(_mode,Constants.AddBookMode.ADD);
            form.MdiParent = this.ParentForm.MdiParent;
            form.Show();
        }

        void EditBookToolStripMenuItemClick(object sender, EventArgs e)
        {
        	foreach(string id in this.SelectedIds)
        	{
        		AddBookForm form = new AddBookForm(_mode, Constants.AddBookMode.EDIT, id);
        		form.MdiParent = this.ParentForm.MdiParent;
        		form.Show();
        	}
        }
        
        void AddMultipleBooksToolStripMenuItemClick(object sender, EventArgs e)
        {
        	BatchAddByISBNForm frm = new BatchAddByISBNForm(20,_mode);
        	frm.MdiParent = this.ParentForm.MdiParent;
        	frm.Show();
        }
    }
}
