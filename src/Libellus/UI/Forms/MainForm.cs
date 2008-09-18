using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using Libellus.Utilities;
using Libellus.Domain;
using Libellus.DataAccess;
using Libellus.Printing;

namespace Libellus.UI.Forms
{
    public partial class MainForm : BaseForm
    {
        #region Private Data
        private FileInfo _currentDB = null;
        private BaseInfo _dbInfo = null;
        private PrintEngine _printEngine = new PrintEngine();
        private Configuration _config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.MdiChildActivate += new EventHandler(MainForm_MdiChildActivate);
            this.checkSettings();
        }

        void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ViewBooksForm || this.ActiveMdiChild is AddBookForm)
                this.SetPrintingMenu(true);
            else
                this.SetPrintingMenu(false);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
		protected override void OnMdiChildActivate(EventArgs e)
		{
			this.columnsToolStripMenuItem.DropDownItems.Clear();
			
			if(this.ActiveMdiChild is ViewBooksForm)
			{
				ViewBooksForm form = this.ActiveMdiChild as ViewBooksForm;
				form.FillViewMenu(this.columnsToolStripMenuItem);
				this.SetPrintingMenu(true);
				if(form.Grid.SelectedIds.Count < 1)
				{
					this.SetLibraryOperations(false,false,false);
					this.SetWishListOperations(false,false);
				}
				
			}
			else if(this.ActiveMdiChild is AddBookForm)
			{
				this.SetPrintingMenu(true);
				this.SetLibraryOperations(false,false,false);
				this.SetWishListOperations(false,false);
			}
			else
			{
				this.SetPrintingMenu(false);
				this.SetLibraryOperations(false,false,false);
				this.SetWishListOperations(false,false);
			}
			
			this.columnsToolStripMenuItem.Enabled = this.columnsToolStripMenuItem.DropDownItems.Count > 0 ? true : false;
		}
		
        /// <summary>
        /// called when the form's Show() method is called
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //if there is a default db, open it, if not, open the view database form
            if (_config.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value == "")
            {
                this.OpenViewDatabaseForm();
            }
            else
            {                
            	FileInfo db = new FileInfo(_config.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value);
                if (db.Exists)
                {
                    this.CurrentDatabase = db;
                    ViewBooksForm form = new ViewBooksForm(Constants.LibraryMode.LIBRARY);
                    form.MdiParent = this;
                    form.Show();
                }
                else
                {
                	_config.AppSettings.Settings[Constants.Settings.DEFAULT_DB].Value = "";
                	_config.Save(System.Configuration.ConfigurationSaveMode.Modified);
                    this.OpenViewDatabaseForm();
                }
            }
        }

        /// <summary>
        /// called when help->about is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomAboutBox box = new CustomAboutBox();
            box.Show();
        }

        /// <summary>
        /// called when file->new is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void libraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDBForm newForm = new NewDBForm();
            newForm.ShowDialog(this);
        }

        /// <summary>
        /// called when file->close is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// called when file->open is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenViewDatabaseForm();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.InitialDirectory = _currentDB.DirectoryName;
            dialog.AddExtension = true;
            dialog.DefaultExt = ".plb";
            dialog.RestoreDirectory = true;
            dialog.Filter = "Library files (*.plb)|*.plb";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                string file = dialog.FileName;
                FileInfo info = null;
                try
                {
                    info = _currentDB.CopyTo(file, true);
                }
                catch (Exception e1)
                {
                    ExceptionHandler.HandleException(e1);
                    MessageBox.Show("An unexpected error occurred while backing up your library.  Check your permissions on the folder you chose and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (info != null && info.Exists)
                {
                    MessageBox.Show("Your database was successfully backed up", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Called when Tools -> Import is Called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Library files (*.plb)|*.plb";
            dialog.RestoreDirectory = true;
            dialog.Multiselect = true;
            bool errors = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo info = new FileInfo(file);
                    if (info.Exists)
                    {
                        BaseInfoDAO dao = new BaseInfoDAO(info.FullName);
                        string version = dao.getVersionNumber();
                        if (version == null)
                        {
                            MessageBox.Show("The database you selected is not supported under this version of the application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        else if (version == "1.0")
                        {
							info.CopyTo(info.Name,true);
							dao = new BaseInfoDAO(info.Name);
							StreamReader sr = File.OpenText("db10_to_12.sql");
							while(!sr.EndOfStream)
							{
								string line = sr.ReadToEnd();
								foreach(string command in line.Split(';'))
								{
									if(!dao.ExecuteNonQuery(command.Trim()))
										errors = true;
								}
							}

							MessageBox.Show("Database successfully imported and updated","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							continue;
                        }
                        else if (version == "1.1")
                        {
							info.CopyTo(info.Name,true);
							dao = new BaseInfoDAO(info.Name);
							StreamReader sr = File.OpenText("db11_to_12.sql");
							while(!sr.EndOfStream)
							{
								string line = sr.ReadToEnd();
								foreach(string command in line.Split(';'))
								{
									string trimmedCommand = command.Trim();
									if(trimmedCommand != "" && !dao.ExecuteNonQuery(trimmedCommand))
										errors = true;
								}
							}

							MessageBox.Show("Database successfully imported and updated","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							continue;
                        }
                        else if(version.Equals("1.2"))
                        {
                            //copy the file directly
                            info.CopyTo(info.Name);
                            MessageBox.Show("Database imported successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("The database you selected is not supported under this version of the application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;  	
                        }
                    }
                }
            }
            
            if(errors == false)
            {
            	foreach(Form f in this.MdiChildren)
            	{
            		if(f is ViewDatabasesForm)
            			(f as ViewDatabasesForm).Refresh();
            	}
            }
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.antmason.com/wiki/index.php/AntMason:Site_support");
            }
            catch (Exception e1)
            {
                ExceptionHandler.HandleException(e1);
                MessageBox.Show("Thank you for your interest in donating to our development fund.  Your default internet browser could not be opened.  Please visit http://www.wikiant.com/wiki/index.php/AntMason:Site_support for more information", "Thank you", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        private void loanBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is ViewBooksForm)
            {
                ViewBooksForm form = this.ActiveMdiChild as ViewBooksForm;

                if (form.LibraryMode == Constants.LibraryMode.LIBRARY)
                {
                    foreach (Book b in form.Grid.SelectedBooks)
                    {
                        LoanBookForm lForm = new LoanBookForm(b.Id, b.ShortTitle, this);
                    }
                }
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _printEngine.PrintableForm = this.ActiveMdiChild;
            _printEngine.PrintSetup();
        }

        private void printPreviewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _printEngine.PrintableForm = this.ActiveMdiChild;
            _printEngine.PrintPreview();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _printEngine.PrintableForm = this.ActiveMdiChild;
            _printEngine.Print();
        }

        /// <summary>
        /// Event handler that cascades the windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// Event handles that tiles the windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Event handler that tiles the windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void viewLoanedBooksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewBooksForm form = new ViewBooksForm(Constants.LibraryMode.LOANEDBOOKS);
            form.MdiParent = this;
            form.Show();
        }

        private void viewWishlistToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewBooksForm form = new ViewBooksForm(Constants.LibraryMode.WISHLIST);
            form.MdiParent = this;
            form.Show();
        }
        
        private void addBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm(Constants.LibraryMode.LIBRARY, Constants.AddBookMode.ADD);
            form.MdiParent = this;
            form.Show();
        }

        private void addBookToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm(Constants.LibraryMode.WISHLIST, Constants.AddBookMode.ADD);
            form.MdiParent = this;
            form.Show();
        }
        
        void ViewLibraryToolStripMenuItemClick(object sender, EventArgs e)
        {
            ViewBooksForm form = new ViewBooksForm(Constants.LibraryMode.LIBRARY);
            form.MdiParent = this;
            form.Show();	
        }
        
        void AddMultipleBooksToolStripMenuItemClick(object sender, EventArgs e)
        {
        	BatchAddByISBNForm form = new BatchAddByISBNForm(20,Constants.LibraryMode.LIBRARY);
        	form.MdiParent = this;
        	form.Show();
        }
        
        void AddMultipleBooksToolStripMenuItem1Click(object sender, EventArgs e)
        {
        	BatchAddByISBNForm form = new BatchAddByISBNForm(20,Constants.LibraryMode.WISHLIST);
        	form.MdiParent = this;
        	form.Show();
        }
        #endregion

        #region Utilities Methods
        private void OpenViewDatabaseForm()
        {
            ViewDatabasesForm form = new ViewDatabasesForm();
            form.MdiParent = this;
            form.Show();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the text in the status bar at the bottom of the screen
        /// </summary>
        /// <param name="text"></param>
        public void SetStatusStripText(string text)
        {
            this.toolStripStatusLabel1.Text = text;
            this.statusStrip1.Refresh();
        }

        public void SetPrintingMenu(bool value)
        {
            this.printPreviewToolStripMenuItem1.Enabled = value;
            this.pageSetupToolStripMenuItem.Enabled = value;
            this.printToolStripMenuItem.Enabled = value;
        }

        /// <summary>
        /// This is the filename of the current database, without the path
        /// </summary>
        public FileInfo CurrentDatabase
        {
            get { return this._currentDB; }
            set 
            { 
                this._currentDB = value;
                if (this._currentDB == null)
                {
                    this.SetStatusStripText("Please click File, Open to open a Library.");
                    this.libraryToolStripMenuItem1.Enabled = false;
                    this.wishListToolStripMenuItem.Enabled = false;
                    this.backupToolStripMenuItem.Enabled = false;
                    this.addBookToolStripMenuItem.Enabled = false;
                    this.addBookToolStripMenuItem1.Enabled = false;
                    this.addMultipleBooksToolStripMenuItem.Enabled = false;
                    this.addMultipleBooksToolStripMenuItem1.Enabled = false;
                }
                else
                {
                    this.libraryToolStripMenuItem1.Enabled = true;
                    this.wishListToolStripMenuItem.Enabled = true;
                    this.addBookToolStripMenuItem.Enabled = true;
                    this.addBookToolStripMenuItem1.Enabled = true;
                    this.addMultipleBooksToolStripMenuItem.Enabled = true;
                    this.addMultipleBooksToolStripMenuItem1.Enabled = true;
                    this.backupToolStripMenuItem.Enabled = true;

                    BaseInfoDAO dao = new BaseInfoDAO(_currentDB.FullName);
                    _dbInfo = dao.GetBaseInfo();
                    this.SetStatusStripText("Currently Viewing " + Utils.GetPossessive(_dbInfo.Owner) + " Library");
                }
            }
        }

        public BaseInfo DBInfo
        {
            get { return _dbInfo; }
            set { _dbInfo = value; }
        }

        public void SetLibraryOperations(bool loan, bool checkin, bool delete)
        {
            this.loanBookToolStripMenuItem.Enabled = loan;
            this.checkInBookToolStripMenuItem.Enabled = checkin;
            this.deleteBookToolStripMenuItem.Enabled = delete;
        }

        public void SetWishListOperations(bool delete, bool move)
        {
            this.deleteBookToolStripMenuItem1.Enabled = delete;
            this.moveBooksToLibraryToolStripMenuItem.Enabled = move;
        }
        
        public Configuration AppSettings
        {
        	get { return _config; }
        }
        #endregion
   
        #region Private Methods
        private void checkSettings()
        {
        	if(_config.AppSettings.Settings[Constants.Settings.DEFAULT_DB] == null)
        		_config.AppSettings.Settings.Add(Constants.Settings.DEFAULT_DB,"");
        }
        #endregion
    
    }
}
