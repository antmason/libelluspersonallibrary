using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Libellus.DataAccess;
using Libellus.Domain;
using Libellus.XML;
using Libellus.Utilities;
using System.Threading;

namespace Libellus.UI.Forms
{
    public partial class BatchAddByISBNForm : BaseForm
    {
        #region Private Data
        TextBox[] _textboxes = null;
        Label[] _resultLabels = null;
        List<Book> _books = new List<Book>();
        Constants.LibraryMode _mode;
        #endregion

        #region Constructor
        public BatchAddByISBNForm(int numBooks, Constants.LibraryMode mode)
        {
            InitializeComponent();
            _textboxes = new TextBox[numBooks];
            _resultLabels = new Label[numBooks];
            _textboxes[0] = this.txtISBN0;
            _resultLabels[0] = this.lblResult0;
            _mode = mode;


            int txty = this.txtISBN0.Location.Y;
            int resulty = this.lblResult0.Location.Y;
            int rowy = this.lblRow0.Location.Y;

            int txtx = this.txtISBN0.Location.X;
            int rowx = this.lblRow0.Location.X;
            int resultx = this.lblResult0.Location.X;

            for (int i = 1; i < numBooks; i++)
            {
                txty += 30;
                rowy += 30;
                resulty += 30;

                if (i == 10)
                {
                    txtx = this.txtISBN0.Location.X + 270;
                    rowx = this.lblRow0.Location.X + 270;
                    resultx = this.lblResult0.Location.X + 270;
                    txty = this.txtISBN0.Location.Y;
                    resulty = this.lblResult0.Location.Y;
                    rowy = this.lblRow0.Location.Y;
                }

                TextBox t = new TextBox();
                t.Name = "txtISBN" + i;
                t.Location = new System.Drawing.Point(txtx, txty);
                t.Size = new System.Drawing.Size(124, 20);
                t.TabIndex = i;
                t.CharacterCasing = CharacterCasing.Upper;
                _textboxes[i] = t;

                Label l = new Label();
                l.Location = new System.Drawing.Point(resultx, resulty);
                l.Name = "lblResult" + i;
                l.Size = new System.Drawing.Size(35, 13);
                l.Text = "lbl";
                l.Visible = false;
                l.AutoSize = true;
                l.Font = new Font(l.Font, FontStyle.Bold);
                _resultLabels[i] = l;

                Label l2 = new Label();
                l2.Location = new System.Drawing.Point(rowx, rowy);
                l2.Name = "lblRow" + i;
                l2.Size = new System.Drawing.Size(35, 13);
                l2.Text = "" + (i + 1) + ". ";

                this.groupBox1.Controls.Add(t);
                this.groupBox1.Controls.Add(l);
                this.groupBox1.Controls.Add(l2);

            }
        }
        #endregion

        #region Event Handlers
        private void btnSave_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)this.MdiParent;
            BookDAO dao = new BookDAO(form.CurrentDatabase.FullName);
            this.clearErrors();
            for (int i = 0; i < _textboxes.Length; i++)
            {

                string isbn = _textboxes[i].Text;
                string uri = Constants.GetWebServiceURI("isbn", isbn);
                if (isbn == null || isbn.Equals(""))
                    continue;

                TextBox txtbox = _textboxes[i];
                Label lbl = _resultLabels[i];

                this.setTextBox(txtbox, lbl, "Processing...", Color.Blue, true, true);

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
	            	this.setTextBox(txtbox,lbl,"No Connection",Color.Red,true,false);
	            	return;
	            }
	            catch (Exception e1)
	            {
	                ExceptionHandler.HandleException(e1);
	                this.setTextBox(txtbox, lbl, "Failure", Color.Red, true, false);
	       			continue;
	            }
            

                Element element = XMLParser.ParseXML(response.GetResponseStream());
#if DEBUG
                string tree = "";
                int level = 0;
                XMLParser.GetParseTreeInfo(element, ref tree, ref level);
                Console.Out.WriteLine(tree);
#endif
                Book book = new Book();
                Constants.XMLResultReturnValue result = book.FillFromXMLResults(element);


                if (result == Constants.XMLResultReturnValue.NOT_FOUND)
                    this.setTextBox(txtbox, lbl, "Not Found", Color.Red, true, false);
                else if (result == Constants.XMLResultReturnValue.UNKNOWN)
                    this.setTextBox(txtbox, lbl, "Failure", Color.Red, true, false);
                else if (book != null)
                {
                    if (_mode == Constants.LibraryMode.LIBRARY && dao.ExistsInLibrary(book))
                        this.setTextBox(txtbox, lbl, "Already Exists", Color.Red, true, false);
                    else if (_mode == Constants.LibraryMode.WISHLIST && dao.ExistsInWishList(book))
                        this.setTextBox(txtbox, lbl, "Already Exists", Color.Red, true, false);
                    else if (_mode == Constants.LibraryMode.LIBRARY && !dao.InsertIntoLibrary(book))
                        this.setTextBox(txtbox, lbl, "Failure", Color.Red, true, false);
                    else if (_mode == Constants.LibraryMode.WISHLIST && !dao.InsertIntoWishList(book))
                        this.setTextBox(txtbox, lbl, "Failure", Color.Red, true, false);
                    else
                    {
                        this.setTextBox(txtbox, lbl, "Success", Color.Green, true, false);

                        foreach (Form f in this.MdiParent.MdiChildren)
                        {
                            if (f is ViewBooksForm)
                                (f as ViewBooksForm).Refresh();
                        }
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _textboxes.Length; i++)
            {
                _textboxes[i].Text = "";
                this.setTaskBar("");
                this.clearLabel(_resultLabels[i], _textboxes[i]);
            }
        }
        #endregion 

        #region Private Utility Methods
        private void setTextBox(TextBox t, Label l, string text, Color c, bool visible, bool blink)
        {
                l.Visible = visible;
                if (l.Visible == false)
                    return;
                l.Text = text;
                l.ForeColor = c;
                if (text.Equals("Not Found"))
                    this.errorProvider1.SetError(t, "This ISBN could not be found.");
                else if (text.Equals("Already Exists"))
                    this.errorProvider1.SetError(t, "This book is already in your library.");
                else if (text.Equals("Failure"))
                    this.errorProvider1.SetError(t, "Error while trying to add this book.");
                else if (text.Equals("No Connection"))
                	this.errorProvider1.SetError(t, ErrorMessages.Common.WEB_SERVICE_ERROR);

                Application.DoEvents();
        }

        private void setTaskBar(string msg)
        {
            MainForm form = (MainForm)this.MdiParent;
            if (msg == null || msg.Equals(""))
                form.SetStatusStripText("Please enter your ISBN numbers and click Submit to continue.");
            else if (msg.Equals("error"))
                form.SetStatusStripText("There were errors adding at least one book, please check your errors by hovering your mouse over the red exclamation mark.");
        }

        private void clearLabel(Label l, TextBox t)
        {
            l.Text = "";
            l.Visible = false;
            this.errorProvider1.SetError(t, "");
        }

        private void clearErrors()
        {
            foreach (TextBox t in _textboxes)
                this.errorProvider1.SetError(t, "");

            foreach (Label l in _resultLabels)
                l.Visible = false;
        }
        #endregion
    }
}
