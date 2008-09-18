namespace Libellus.UI.Controls
{
    partial class BookGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	this.gridBooks = new System.Windows.Forms.DataGridView();
        	this.short_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.date_loaned = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.LoanedTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.date_checkedin = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.AuthorFirst = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.AuthorLast = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.publisher_info = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.isbn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.edition_info = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.date_added = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colUsedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colNewPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colLCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colDewey = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colDeweyNorm = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colPhysDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colEdition = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colAwards = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        	this.addBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.addMultipleBooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.deleteBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.viewDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.editBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.loanBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.checkDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.viewLoanHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.transferBookToLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.transferBookToWishListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.bookDAOBindingSource = new System.Windows.Forms.BindingSource(this.components);
        	((System.ComponentModel.ISupportInitialize)(this.gridBooks)).BeginInit();
        	this.contextMenuStrip1.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.bookDAOBindingSource)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// gridBooks
        	// 
        	this.gridBooks.AllowUserToAddRows = false;
        	this.gridBooks.AllowUserToDeleteRows = false;
        	this.gridBooks.AllowUserToOrderColumns = true;
        	this.gridBooks.AutoGenerateColumns = false;
        	this.gridBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        	this.gridBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        	this.gridBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
        	        	        	this.short_title,
        	        	        	this.date_loaned,
        	        	        	this.LoanedTo,
        	        	        	this.date_checkedin,
        	        	        	this.Id,
        	        	        	this.AuthorFirst,
        	        	        	this.AuthorLast,
        	        	        	this.subject,
        	        	        	this.publisher_info,
        	        	        	this.isbn,
        	        	        	this.edition_info,
        	        	        	this.date_added,
        	        	        	this.colUsedPrice,
        	        	        	this.colNewPrice,
        	        	        	this.colLCC,
        	        	        	this.colDewey,
        	        	        	this.colDeweyNorm,
        	        	        	this.colPhysDesc,
        	        	        	this.colEdition,
        	        	        	this.colLanguage,
        	        	        	this.colNotes,
        	        	        	this.colAwards});
        	this.gridBooks.ContextMenuStrip = this.contextMenuStrip1;
        	this.gridBooks.DataSource = this.bookDAOBindingSource;
        	this.gridBooks.Location = new System.Drawing.Point(0, 0);
        	this.gridBooks.Margin = new System.Windows.Forms.Padding(0);
        	this.gridBooks.Name = "gridBooks";
        	this.gridBooks.ReadOnly = true;
        	this.gridBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        	this.gridBooks.ShowEditingIcon = false;
        	this.gridBooks.Size = new System.Drawing.Size(841, 290);
        	this.gridBooks.TabIndex = 1;
        	this.gridBooks.VirtualMode = true;
        	// 
        	// short_title
        	// 
        	this.short_title.DataPropertyName = "short_title";
        	this.short_title.HeaderText = "Title";
        	this.short_title.Name = "short_title";
        	this.short_title.ReadOnly = true;
        	// 
        	// date_loaned
        	// 
        	this.date_loaned.DataPropertyName = "date_loaned";
        	this.date_loaned.HeaderText = "Loan Date";
        	this.date_loaned.Name = "date_loaned";
        	this.date_loaned.ReadOnly = true;
        	this.date_loaned.Visible = false;
        	// 
        	// LoanedTo
        	// 
        	this.LoanedTo.DataPropertyName = "loan_to";
        	this.LoanedTo.HeaderText = "Loaned To";
        	this.LoanedTo.Name = "LoanedTo";
        	this.LoanedTo.ReadOnly = true;
        	this.LoanedTo.Visible = false;
        	// 
        	// date_checkedin
        	// 
        	this.date_checkedin.DataPropertyName = "date_checkedin";
        	this.date_checkedin.HeaderText = "Checked In";
        	this.date_checkedin.Name = "date_checkedin";
        	this.date_checkedin.ReadOnly = true;
        	this.date_checkedin.Visible = false;
        	// 
        	// Id
        	// 
        	this.Id.DataPropertyName = "id";
        	this.Id.HeaderText = "Id";
        	this.Id.Name = "Id";
        	this.Id.ReadOnly = true;
        	this.Id.Visible = false;
        	// 
        	// AuthorFirst
        	// 
        	this.AuthorFirst.DataPropertyName = "first_name";
        	this.AuthorFirst.HeaderText = "Author First Name";
        	this.AuthorFirst.Name = "AuthorFirst";
        	this.AuthorFirst.ReadOnly = true;
        	// 
        	// AuthorLast
        	// 
        	this.AuthorLast.DataPropertyName = "last_name";
        	this.AuthorLast.HeaderText = "Author Last Name";
        	this.AuthorLast.Name = "AuthorLast";
        	this.AuthorLast.ReadOnly = true;
        	// 
        	// subject
        	// 
        	this.subject.DataPropertyName = "subject";
        	this.subject.HeaderText = "Subject";
        	this.subject.Name = "subject";
        	this.subject.ReadOnly = true;
        	// 
        	// publisher_info
        	// 
        	this.publisher_info.DataPropertyName = "publisher_info";
        	this.publisher_info.HeaderText = "Publisher";
        	this.publisher_info.Name = "publisher_info";
        	this.publisher_info.ReadOnly = true;
        	// 
        	// isbn
        	// 
        	this.isbn.DataPropertyName = "isbn";
        	this.isbn.HeaderText = "ISBN";
        	this.isbn.Name = "isbn";
        	this.isbn.ReadOnly = true;
        	// 
        	// edition_info
        	// 
        	this.edition_info.DataPropertyName = "edition_info";
        	this.edition_info.HeaderText = "Edition";
        	this.edition_info.Name = "edition_info";
        	this.edition_info.ReadOnly = true;
        	// 
        	// date_added
        	// 
        	this.date_added.DataPropertyName = "date_added";
        	this.date_added.HeaderText = "Date Added";
        	this.date_added.Name = "date_added";
        	this.date_added.ReadOnly = true;
        	// 
        	// colUsedPrice
        	// 
        	this.colUsedPrice.DataPropertyName = "used_price";
        	this.colUsedPrice.HeaderText = "Avg. Used Price";
        	this.colUsedPrice.Name = "colUsedPrice";
        	this.colUsedPrice.ReadOnly = true;
        	this.colUsedPrice.Visible = false;
        	// 
        	// colNewPrice
        	// 
        	this.colNewPrice.DataPropertyName = "new_price";
        	this.colNewPrice.HeaderText = "Avg. New Price";
        	this.colNewPrice.Name = "colNewPrice";
        	this.colNewPrice.ReadOnly = true;
        	this.colNewPrice.Visible = false;
        	// 
        	// colLCC
        	// 
        	this.colLCC.DataPropertyName = "lcc_number";
        	this.colLCC.HeaderText = "LCC";
        	this.colLCC.Name = "colLCC";
        	this.colLCC.ReadOnly = true;
        	this.colLCC.Visible = false;
        	// 
        	// colDewey
        	// 
        	this.colDewey.DataPropertyName = "dewey";
        	this.colDewey.HeaderText = "Dewey Dec.";
        	this.colDewey.Name = "colDewey";
        	this.colDewey.ReadOnly = true;
        	this.colDewey.Visible = false;
        	// 
        	// colDeweyNorm
        	// 
        	this.colDeweyNorm.DataPropertyName = "dewey_norm";
        	this.colDeweyNorm.HeaderText = "Dewey Norm.";
        	this.colDeweyNorm.Name = "colDeweyNorm";
        	this.colDeweyNorm.ReadOnly = true;
        	this.colDeweyNorm.Visible = false;
        	// 
        	// colPhysDesc
        	// 
        	this.colPhysDesc.DataPropertyName = "physical_desc";
        	this.colPhysDesc.HeaderText = "Phys. Desc.";
        	this.colPhysDesc.Name = "colPhysDesc";
        	this.colPhysDesc.ReadOnly = true;
        	this.colPhysDesc.Visible = false;
        	// 
        	// colEdition
        	// 
        	this.colEdition.DataPropertyName = "edition_info";
        	this.colEdition.HeaderText = "Edition";
        	this.colEdition.Name = "colEdition";
        	this.colEdition.ReadOnly = true;
        	this.colEdition.Visible = false;
        	// 
        	// colLanguage
        	// 
        	this.colLanguage.DataPropertyName = "language";
        	this.colLanguage.HeaderText = "Language";
        	this.colLanguage.Name = "colLanguage";
        	this.colLanguage.ReadOnly = true;
        	this.colLanguage.Visible = false;
        	// 
        	// colNotes
        	// 
        	this.colNotes.DataPropertyName = "notes";
        	this.colNotes.HeaderText = "Notes";
        	this.colNotes.Name = "colNotes";
        	this.colNotes.ReadOnly = true;
        	this.colNotes.Visible = false;
        	// 
        	// colAwards
        	// 
        	this.colAwards.DataPropertyName = "awards";
        	this.colAwards.HeaderText = "Awards";
        	this.colAwards.Name = "colAwards";
        	this.colAwards.ReadOnly = true;
        	this.colAwards.Visible = false;
        	// 
        	// contextMenuStrip1
        	// 
        	this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        	        	        	this.addBookToolStripMenuItem,
        	        	        	this.addMultipleBooksToolStripMenuItem,
        	        	        	this.deleteBookToolStripMenuItem,
        	        	        	this.viewDetailsToolStripMenuItem,
        	        	        	this.editBookToolStripMenuItem,
        	        	        	this.loanBookToolStripMenuItem,
        	        	        	this.checkDetailsToolStripMenuItem,
        	        	        	this.viewLoanHistoryToolStripMenuItem,
        	        	        	this.transferBookToLibraryToolStripMenuItem,
        	        	        	this.transferBookToWishListToolStripMenuItem});
        	this.contextMenuStrip1.Name = "contextMenuStrip1";
        	this.contextMenuStrip1.Size = new System.Drawing.Size(213, 246);
        	this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
        	// 
        	// addBookToolStripMenuItem
        	// 
        	this.addBookToolStripMenuItem.Name = "addBookToolStripMenuItem";
        	this.addBookToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.addBookToolStripMenuItem.Text = "Add Book";
        	this.addBookToolStripMenuItem.Click += new System.EventHandler(this.addBookToolStripMenuItem_Click);
        	// 
        	// addMultipleBooksToolStripMenuItem
        	// 
        	this.addMultipleBooksToolStripMenuItem.Name = "addMultipleBooksToolStripMenuItem";
        	this.addMultipleBooksToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.addMultipleBooksToolStripMenuItem.Text = "Add Multiple Books";
        	this.addMultipleBooksToolStripMenuItem.Click += new System.EventHandler(this.AddMultipleBooksToolStripMenuItemClick);
        	// 
        	// deleteBookToolStripMenuItem
        	// 
        	this.deleteBookToolStripMenuItem.Enabled = false;
        	this.deleteBookToolStripMenuItem.Name = "deleteBookToolStripMenuItem";
        	this.deleteBookToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.deleteBookToolStripMenuItem.Text = "Delete Book";
        	this.deleteBookToolStripMenuItem.Click += new System.EventHandler(this.deleteBookToolStripMenuItem_Click);
        	// 
        	// viewDetailsToolStripMenuItem
        	// 
        	this.viewDetailsToolStripMenuItem.Enabled = false;
        	this.viewDetailsToolStripMenuItem.Name = "viewDetailsToolStripMenuItem";
        	this.viewDetailsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.viewDetailsToolStripMenuItem.Text = "View Details";
        	this.viewDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewDetailsToolStripMenuItem_Click);
        	// 
        	// editBookToolStripMenuItem
        	// 
        	this.editBookToolStripMenuItem.Enabled = false;
        	this.editBookToolStripMenuItem.Name = "editBookToolStripMenuItem";
        	this.editBookToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.editBookToolStripMenuItem.Text = "Edit Book";
        	this.editBookToolStripMenuItem.Click += new System.EventHandler(this.EditBookToolStripMenuItemClick);
        	// 
        	// loanBookToolStripMenuItem
        	// 
        	this.loanBookToolStripMenuItem.Enabled = false;
        	this.loanBookToolStripMenuItem.Name = "loanBookToolStripMenuItem";
        	this.loanBookToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.loanBookToolStripMenuItem.Text = "Loan Book";
        	this.loanBookToolStripMenuItem.Click += new System.EventHandler(this.loanBookToolStripMenuItem_Click);
        	// 
        	// checkDetailsToolStripMenuItem
        	// 
        	this.checkDetailsToolStripMenuItem.Enabled = false;
        	this.checkDetailsToolStripMenuItem.Name = "checkDetailsToolStripMenuItem";
        	this.checkDetailsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.checkDetailsToolStripMenuItem.Text = "Check In Book";
        	this.checkDetailsToolStripMenuItem.Click += new System.EventHandler(this.checkDetailsToolStripMenuItem_Click);
        	// 
        	// viewLoanHistoryToolStripMenuItem
        	// 
        	this.viewLoanHistoryToolStripMenuItem.Enabled = false;
        	this.viewLoanHistoryToolStripMenuItem.Name = "viewLoanHistoryToolStripMenuItem";
        	this.viewLoanHistoryToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.viewLoanHistoryToolStripMenuItem.Text = "View Loan History";
        	this.viewLoanHistoryToolStripMenuItem.Click += new System.EventHandler(this.viewLoanHistoryToolStripMenuItem_Click);
        	// 
        	// transferBookToLibraryToolStripMenuItem
        	// 
        	this.transferBookToLibraryToolStripMenuItem.Enabled = false;
        	this.transferBookToLibraryToolStripMenuItem.Name = "transferBookToLibraryToolStripMenuItem";
        	this.transferBookToLibraryToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.transferBookToLibraryToolStripMenuItem.Text = "Transfer Book To Library";
        	this.transferBookToLibraryToolStripMenuItem.Visible = false;
        	this.transferBookToLibraryToolStripMenuItem.Click += new System.EventHandler(this.transferBookToLibraryToolStripMenuItem_Click);
        	// 
        	// transferBookToWishListToolStripMenuItem
        	// 
        	this.transferBookToWishListToolStripMenuItem.Enabled = false;
        	this.transferBookToWishListToolStripMenuItem.Name = "transferBookToWishListToolStripMenuItem";
        	this.transferBookToWishListToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
        	this.transferBookToWishListToolStripMenuItem.Text = "Transfer Book To Wish List";
        	this.transferBookToWishListToolStripMenuItem.Visible = false;
        	this.transferBookToWishListToolStripMenuItem.Click += new System.EventHandler(this.transferBookToWishListToolStripMenuItem_Click);
        	// 
        	// bookDAOBindingSource
        	// 
        	this.bookDAOBindingSource.DataSource = typeof(Libellus.DataAccess.BookDAO);
        	// 
        	// BookGrid
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.AutoSize = true;
        	this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        	this.Controls.Add(this.gridBooks);
        	this.Margin = new System.Windows.Forms.Padding(0);
        	this.Name = "BookGrid";
        	this.Size = new System.Drawing.Size(841, 290);
        	((System.ComponentModel.ISupportInitialize)(this.gridBooks)).EndInit();
        	this.contextMenuStrip1.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.bookDAOBindingSource)).EndInit();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.ToolStripMenuItem editBookToolStripMenuItem;

        #endregion

        private System.Windows.Forms.DataGridView gridBooks;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loanBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkDetailsToolStripMenuItem;
        private System.Windows.Forms.BindingSource bookDAOBindingSource;
        private System.Windows.Forms.ToolStripMenuItem viewLoanHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferBookToLibraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferBookToWishListToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn short_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_loaned;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanedTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_checkedin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuthorFirst;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuthorLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn publisher_info;
        private System.Windows.Forms.DataGridViewTextBoxColumn isbn;
        private System.Windows.Forms.DataGridViewTextBoxColumn edition_info;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_added;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsedPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDewey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeweyNorm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhysDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAwards;
        private System.Windows.Forms.ToolStripMenuItem addMultipleBooksToolStripMenuItem;
    }
}
