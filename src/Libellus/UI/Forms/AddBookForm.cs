using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Libellus.Utilities;
using Libellus.XML;
using Libellus.Domain;
using System.IO;
using Libellus.DataAccess;

namespace Libellus.UI.Forms
{
    public partial class AddBookForm : BaseForm
    {
        #region Private Data
        private Book _book = new Book();
        private Constants.LibraryMode _libraryMode;
        private Constants.AddBookMode _addMode;
        private string _bookId;

        #endregion

        #region Constructor
        public AddBookForm(Constants.LibraryMode libMode,Constants.AddBookMode addMode)
        {
            _libraryMode = libMode;
            _addMode = addMode;

            InitializeComponent();
            //this.chkAutoAdd.CheckedChanged += new EventHandler(chkAutoAdd_CheckedChanged);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnClear.Click += new EventHandler(btnClear_Click);
            this.btnLookUp.Click += new EventHandler(btnLookUp_Click);
            this.cboLookupBy.Items.Add(new DDItem("ISBN","isbn"));
           	// TODO: Readd and test
            // this.cboLookupBy.Items.Add(new DDItem("Lib. Congr. #", "lcc_number"));
            this.cboLookupBy.SelectedIndex = 0;
            
            if (_addMode == Constants.AddBookMode.VIEWONLY)
            {
                this.chkAutoAdd.Visible = false;
                this.btnClear.Visible = false;
                this.btnLookUp.Visible = false;
                this.btnSave.Visible = false;
                this.txtAuthors.ReadOnly = true;
                this.txtAwards.ReadOnly = true;
                this.txtDewey.ReadOnly = true;
                this.txtDeweyNorm.ReadOnly = true;
                this.txtEdition.ReadOnly = true;
                this.txtEditors.ReadOnly = true;
                this.txtISBN.ReadOnly = true;
                this.txtLanguage.ReadOnly = true;
                this.txtLCC.ReadOnly = true;
                this.txtLongTitle.ReadOnly = true;
                this.txtNotes.ReadOnly = true;
                this.txtPhysDesc.ReadOnly = true;
                this.txtPublisher.ReadOnly = true;
                this.txtShortTitle.ReadOnly = true;
                this.txtSubjects.ReadOnly = true;
                this.txtSummary.ReadOnly = true;
                this.txtURLs.ReadOnly = true;
                this.txtNewPrice.ReadOnly = true;
                this.txtUsedPrice.ReadOnly = true;
                this.txtPricePaid.ReadOnly = true;

                this.groupBox1.Text = "View Book";
            }
            else if (_addMode == Constants.AddBookMode.EDIT)
            {
                this.groupBox2.Enabled = false;
            }

            this.toolTip1.SetToolTip(this.txtAuthors,"Put each author on a separate line, preferably in the format 'LastName, FirstName MI'");
            this.toolTip1.SetToolTip(this.txtEditors,"Put each editor on a separate line, preferably in the format 'LastName, FirstName MI'");
            this.toolTip1.SetToolTip(this.txtSubjects,"Put each subject on a separate line");
        }
        
        public AddBookForm(Constants.LibraryMode libMode, Constants.AddBookMode addMode, string bookId)
        {
        	_libraryMode = libMode;
            _addMode = addMode;

            InitializeComponent();
            //this.chkAutoAdd.CheckedChanged += new EventHandler(chkAutoAdd_CheckedChanged);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnClear.Click += new EventHandler(btnClear_Click);
            this.btnLookUp.Click += new EventHandler(btnLookUp_Click);
            this.cboLookupBy.Items.Add(new DDItem("ISBN","isbn"));
           	// TODO: Readd and test
            // this.cboLookupBy.Items.Add(new DDItem("Lib. Congr. #", "lcc_number"));
            this.cboLookupBy.SelectedIndex = 0;
            
            if (_addMode == Constants.AddBookMode.VIEWONLY)
            {
                this.chkAutoAdd.Visible = false;
                this.btnClear.Visible = false;
                this.btnLookUp.Visible = false;
                this.btnSave.Visible = false;
                this.txtAuthors.ReadOnly = true;
                this.txtAwards.ReadOnly = true;
                this.txtDewey.ReadOnly = true;
                this.txtDeweyNorm.ReadOnly = true;
                this.txtEdition.ReadOnly = true;
                this.txtEditors.ReadOnly = true;
                this.txtISBN.ReadOnly = true;
                this.txtLanguage.ReadOnly = true;
                this.txtLCC.ReadOnly = true;
                this.txtLongTitle.ReadOnly = true;
                this.txtNotes.ReadOnly = true;
                this.txtPhysDesc.ReadOnly = true;
                this.txtPublisher.ReadOnly = true;
                this.txtShortTitle.ReadOnly = true;
                this.txtSubjects.ReadOnly = true;
                this.txtSummary.ReadOnly = true;
                this.txtURLs.ReadOnly = true;
                this.txtNewPrice.ReadOnly = true;
                this.txtUsedPrice.ReadOnly = true;
                this.txtPricePaid.ReadOnly = true;

                this.groupBox1.Text = "View Book";
            }
            else if (_addMode == Constants.AddBookMode.EDIT)
            {
                this.groupBox2.Enabled = false;
                this.btnClear.Enabled = false;
            }

            this.toolTip1.SetToolTip(this.txtAuthors,"Put each author on a separate line, preferably in the format 'LastName, FirstName MI'");
            this.toolTip1.SetToolTip(this.txtEditors,"Put each editor on a separate line, preferably in the format 'LastName, FirstName MI'");
            this.toolTip1.SetToolTip(this.txtSubjects,"Put each subject on a separate line");
            
            _bookId = bookId;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Called when Look Up button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookUp_Click(object sender, EventArgs e)
        {
            if (this.txtLookup.Text.Equals(""))
                this.errorProvider1.SetError(this.txtLookup, "Fill in data to lookup");
            else
                this.errorProvider1.SetError(this.txtLookup, "");

            this.errorProvider1.SetError(this.txtISBN, "");
            this.lblResult.Text = "Processing...";
            this.lblResult.ForeColor = Color.Blue;
            this.lblResult.Visible = true;
            Application.DoEvents();
           
            string uri = Constants.GetWebServiceURI((this.cboLookupBy.SelectedItem as DDItem).Value, this.txtLookup.Text);
            WebRequest request = null;
            WebResponse response = null;
            
            try
            {
                request = WebRequest.Create(uri);
                response = request.GetResponse();
            }
            catch(WebException e2)
            {
            	MessageBox.Show(ErrorMessages.Common.WEB_SERVICE_ERROR, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            	this.lblResult.Text = "No Connection";
            	this.lblResult.ForeColor = Color.Red;
            	this.lblResult.Visible = true;
            	return;
            }
            catch (Exception e1)
            {
                ExceptionHandler.HandleException(e1);
                this.lblResult.Text = "Failed";
            	this.lblResult.ForeColor = Color.Red;
            	this.lblResult.Visible = true;
            	return;
            }
 

            Element element = XMLParser.ParseXML(response.GetResponseStream());
#if DEBUG 
            string tree = "";
            int level = 0;
            XMLParser.GetParseTreeInfo(element,ref tree,ref level);
            Console.Out.WriteLine(tree);
#endif
            this.lblResult.Text = "";
            this.lblResult.Visible = false;
            _book = new Book();
            Constants.XMLResultReturnValue result = _book.FillFromXMLResults(element);
            if (result == Constants.XMLResultReturnValue.BOOK_ALREADY_EXISTS)
            {
                this.lblResult.Text = "Failed";
                this.lblResult.ForeColor = Color.Red;
                this.lblResult.Visible = true;
                this.errorProvider1.SetError(this.txtLookup, "This book already exists in your library");
            }
            else if (result == Constants.XMLResultReturnValue.NOT_FOUND)
            {
                this.lblResult.Text = "Not Found";
                this.lblResult.ForeColor = Color.Red;
                this.lblResult.Visible = true;
            }
            else
            {
                this.errorProvider1.SetError(this.txtLookup, "");
                this.Refresh();
            }

            if (this.chkAutoAdd.Checked == true && result == Constants.XMLResultReturnValue.SUCCESS)
            {
                this.save();
                this.clear();
            }

        }
        
        /// <summary>
        /// Caled when "Auto Add" check box is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAutoAdd.Checked == true)
                this.btnSave.Enabled = false;
            else
                this.btnSave.Enabled = true;
        }

        /// <summary>
        /// Called when the clear button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.clear();
            this.setInfoLabel("");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (_addMode == Constants.AddBookMode.VIEWONLY)
                this.setInfoLabel("View");
        }

        /// <summary>
        /// called when "save" is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.isRequiredFieldsFilled())
                return;

            this.setInfoLabel("Processing...");

        
            if (this.chkAutoAdd.Checked == false || _addMode == Constants.AddBookMode.EDIT)
            {
                this.fillDataFromForm();
            }
            
            this.save();
        }
        #endregion

        #region Private Utility Methods

        private void fillDataFromForm()
        {
            string[] authors = this.txtAuthors.Text.Replace("\r\n",";").Split(new char[]{ ';' });
            string[] editors = this.txtEditors.Text.Replace("\r\n", ";").Split(new char[] { ';' });
            string[] subjects = this.txtSubjects.Text.Replace("\r\n", ";").Split(new char[] { ';' });
            
            _book.Authors.Clear();
            _book.Editors.Clear();
            _book.Subjects.Clear();
            
            foreach(string s in authors)
            {
                if(s.Equals("")) 
                    continue;
                Person p = new Person();
                p.FirstName = Person.GetFirstNameFromFull(s);
                p.LastName = Person.GetLastNameFromFull(s);
                p.Id = Utils.CreateIdFromString(p.FullName);
                _book.Authors.Add(p);
            }

            foreach (string s in editors)
            {
                if (s.Equals(""))
                    continue;
                Person p = new Person();
                p.FirstName = Person.GetFirstNameFromFull(s);
                p.LastName = Person.GetLastNameFromFull(s);
                p.Id = Utils.CreateIdFromString(p.FullName);
                _book.Editors.Add(p);
            }

            foreach (string s in subjects)
            {
                if (s.Equals(""))
                    continue;
                Subject su = new Subject();
                su.Name = s;
                su.Id = Utils.CreateIdFromString(s);
                _book.Subjects.Add(su);
            }

            _book.ShortTitle = this.txtShortTitle.Text.Trim();
            _book.LongTitle = this.txtLongTitle.Text.Trim();
            _book.ISBN = this.txtISBN.Text;
            _book.Language = this.txtLanguage.Text;
            _book.LibraryOfCongress = this.txtLCC.Text;
            _book.Notes = this.txtNotes.Text;
            _book.PhysicalDescription = this.txtPhysDesc.Text;
            _book.PublisherInfo = this.txtPublisher.Text;
            if (!_book.PublisherInfo.Equals(""))
                _book.PublisherId = Utils.CreateIdFromString(_book.PublisherInfo);

            _book.Summary = this.txtSummary.Text;
            _book.Urls = this.txtURLs.Text;
            _book.Awards = this.txtAwards.Text;
            _book.DateAdded = DateTime.Now.ToShortDateString();
            _book.Dewey = this.txtDewey.Text;
            _book.NewPrice = this.txtNewPrice.Text;
            _book.UsedPrice = this.txtUsedPrice.Text;
            _book.PricePaid = this.txtPricePaid.Text;
            _book.DeweyNormalized = this.txtDeweyNorm.Text;
            _book.Edition = this.txtEdition.Text;
            string title = this.txtShortTitle.Text.Equals("") ? this.txtLongTitle.Text : this.txtShortTitle.Text;
            _book.Id = Utils.CreateIdFromString(title);
        }

        private bool isRequiredFieldsFilled()
        {
            int error = 0;
            if (this.txtShortTitle.Text.Equals("") && this.txtLongTitle.Text.Equals(""))
            {
                this.errorProvider1.SetError(this.txtShortTitle, "You must fill in at least one of the Title fields");
                this.errorProvider1.SetError(this.txtLongTitle, "You must fill in at least one of the Title fields");
                error++;
            }
            else
            {
                this.errorProvider1.SetError(this.txtShortTitle, "");
                this.errorProvider1.SetError(this.txtLongTitle, "");
            }

            if (error > 0)
            {
                MainForm form = (MainForm)this.MdiParent;
                form.SetStatusStripText("Error: Please fill in all required fields.");
                return false;
            }
            else
            {
                MainForm form = (MainForm)this.MdiParent;
                form.SetStatusStripText("Fill in information and click save when ready.");
                return true;
            }

        }

        /// <summary>
        /// Erm...well...it saves.
        /// </summary>
        private void save()
        {
            if (!this.isRequiredFieldsFilled())
                return;

            MainForm parent = (MainForm)this.MdiParent;
            BookDAO dao = new BookDAO(parent.CurrentDatabase.FullName);
            
            if(_addMode == Constants.AddBookMode.EDIT)
            {
            	if(dao.Updatebook(_book))
            		this.setInfoLabel("Success");
            	else
            		this.setInfoLabel("Failed");
            	return;
            }

            if (dao.ExistsInLibrary(_book))
            {
                if (this.chkAutoAdd.Checked == true)
                    this.errorProvider1.SetError(this.txtLookup, ErrorMessages.Common.BOOK_EXISTS_IN_LIBRARY);
                else
                    this.errorProvider1.SetError(this.txtShortTitle, ErrorMessages.Common.BOOK_EXISTS_IN_LIBRARY);
              
                this.setInfoLabel("Failed");
                this.btnClear.Focus();
                return;
            }
            else
            {
                this.errorProvider1.SetError(this.txtShortTitle, "");
                this.errorProvider1.SetError(this.txtLookup, "");
            }

            if (dao.InsertIntoLibrary(_book))
            {
                this.setInfoLabel("Success");
                this.clear();
                this.errorProvider1.SetError(this.txtShortTitle, "");
                this.errorProvider1.SetError(this.txtLookup, "");
                foreach (Form f in this.MdiParent.MdiChildren)
                {
                    if (f is ViewBooksForm)
                    {
                        ViewBooksForm frm = (ViewBooksForm)f;
                        frm.Refresh();
                    }
                }
            }
            else
            {
                this.setInfoLabel("Failed");
            }
 
        }

        /// <summary>
        /// resets the data and form
        /// </summary>
        private void clear()
        {
            this._book = new Book();
            this.Refresh();
            this.errorProvider1.SetError(this.txtISBN, "");
            this.txtLookup.Focus();
        }

        private void setInfoLabel(string msg)
        {
            Label lbl;
            if (this.chkAutoAdd.Checked == true)
                lbl = this.lblResult;
            else
                lbl = this.lblResult2;

            MainForm parent = (MainForm)this.MdiParent;
            string title = _book.ShortTitle.Equals("") ? _book.LongTitle : _book.ShortTitle;

            if (msg == null || msg.Equals(""))
            {
                lbl.Visible = false;
                parent.SetStatusStripText("Enter an ISBN and click Lookup to proceed");
            }
            else if (msg.Equals("Success"))
            {
                lbl.Text = msg;
                lbl.ForeColor = Color.Green;
                lbl.Visible = true;
                parent.SetStatusStripText("Book '" + title + "' was successfully added");
            }
            else if (msg.Equals("Processing..."))
            {
                lbl.Text = msg;
                lbl.ForeColor = Color.Blue;
                lbl.Visible = true;
            }
            else if (msg.Equals("Failed"))
            {
                lbl.Text = msg;
                lbl.ForeColor = Color.Red;
                lbl.Visible = true;
                parent.SetStatusStripText("Adding of book " + title + " failed.");
            }
            else if (msg.Equals("Not Found"))
            {
                lbl.Text = msg;
                lbl.ForeColor = Color.Red;
                lbl.Visible = true;
                parent.SetStatusStripText("The ISBN you entered was not found.");
            }
            else if (msg.Equals("View"))
            {
                parent.SetStatusStripText("Viewing Book: " + title);
            }

            Application.DoEvents();

        }

        #endregion

        #region Overridden Methods
        /// <summary>
        /// We override refresh to refresh the text boxees from the data
        /// </summary>
        public override void Refresh()
        {
        	if(_bookId != null)
        	{
        		MainForm frm = this.MdiParent as MainForm;
            	BookDAO dao = new BookDAO(frm.CurrentDatabase.Name);
            	this._book = dao.GetBookById(_bookId);
        	}
            Book b = _book;
            base.Refresh();
            txtISBN.Text = b.ISBN;
            txtShortTitle.Text = b.ShortTitle;
            txtLongTitle.Text = b.LongTitle;
            string authors = "";
            foreach (Person p in b.Authors)
                authors += p.FullName + Environment.NewLine;
            txtAuthors.Text = authors;
            string editors = "";
            foreach (Person p in b.Editors)
                editors += p.FullName + Environment.NewLine;
            txtEditors.Text = editors;
            txtPublisher.Text = b.PublisherInfo;
            txtDewey.Text = b.Dewey;
            txtDeweyNorm.Text = b.DeweyNormalized;
            txtLCC.Text = b.LibraryOfCongress;
            txtPhysDesc.Text = b.PhysicalDescription;
            txtEdition.Text = b.Edition;
            txtLanguage.Text = b.Language;
            txtSummary.Text = b.Summary;
            txtNotes.Text = b.Notes;
            txtAwards.Text = b.Awards;
            txtURLs.Text = b.Urls;
            txtNewPrice.Text = b.NewPrice;
            txtUsedPrice.Text = b.UsedPrice;
            string subjects = "";
            foreach (Subject s in b.Subjects)
            {
                subjects += s.Name + "\r\n";
            }
            txtSubjects.Text = subjects;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets and sets the domain object that is represented by the form
        /// </summary>
        public Book BookData
        {
            get { return _book; }
            set { _book = value; this.Refresh(); }
        }
        #endregion 
        
		protected override void OnLoad(EventArgs e)
		{
			if(_bookId != null)
				this.Refresh();
			base.OnLoad(e);
		}

    }
}
