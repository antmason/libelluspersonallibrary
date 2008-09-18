using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using Libellus.UI.Forms;
using Libellus.UI.Controls;
using Libellus.Domain;

namespace Libellus.Printing
{
    public class PrintEngine
    {
        private PrintDocument _document = new PrintDocument();
        private PrintPreviewDialog _previewDialog = new PrintPreviewDialog();
        private PrintDialog _printDialog = new PrintDialog();
        private PageSetupDialog _setupDialog = new PageSetupDialog();
        private Form _form;
        private int _pageNumber = 1;
        private int _record = 0;
        private List<int> _columns = new List<int>();
        private PrintPageEventArgs _printEvent = null;
        private Font _headerFont;
        private Font _regularFont;
        private Font _boldFont;
        private StringFormat _rightAlign;
        private StringFormat _format;
        private StringFormat _wrap;
        private int _left;
        private int _right;
        private int _top;
        private int _bottom;
        private Brush _brush;
        private float _x = 0;
        private float _y = 0;

        public PrintEngine()
        {
            _document.DefaultPageSettings.Margins.Left = 50;
            _document.DefaultPageSettings.Margins.Right = 50;
            _document.DefaultPageSettings.Margins.Top = 50;
            _document.DefaultPageSettings.Margins.Bottom = 50;
            _previewDialog.Document = _document;
            _printDialog.Document = _document;
            _setupDialog.Document = _document;
            _document.PrintPage += new PrintPageEventHandler(_document_PrintPage);
            _headerFont = new Font("Times New Roman", 10,FontStyle.Bold | FontStyle.Italic);
            _regularFont = new Font("Arial", 8);
            _boldFont = new Font("Arial", 10, FontStyle.Bold);
            _rightAlign = new StringFormat();
            _rightAlign.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            _rightAlign.Trimming = StringTrimming.EllipsisCharacter;
        
            _format = new StringFormat();
            _format.FormatFlags = StringFormatFlags.NoWrap;
            _wrap = new StringFormat();
            _format.Trimming = StringTrimming.EllipsisCharacter;
            _wrap.Trimming = StringTrimming.EllipsisCharacter;
            _brush = Brushes.Black;
        }

        public Form PrintableForm
        {
            set { _form = value; }
        }

        void _document_PrintPage(object sender, PrintPageEventArgs e)
        {
            _printEvent = e;
            _left = e.MarginBounds.Left;
            _right = e.MarginBounds.Right;
            _top = e.MarginBounds.Top;
            _bottom = e.MarginBounds.Bottom;
            Console.Out.WriteLine(_pageNumber);
            this.printHeader();
            if (_form is ViewBooksForm)
            {
                ViewBooksForm f = _form as ViewBooksForm;
                this.printDataGrid(f.DataGrid);
            }
            else if (_form is AddBookForm)
            {
                AddBookForm f = _form as AddBookForm;
                this.printDomainObject(f.BookData);
            }
            this.printFooter();
        }

        public void Print()
        {
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                _document.Print();
            }
        }

        public void PrintPreview()
        {
            _previewDialog.ShowDialog();
        }

        public void PrintSetup()
        {
            if (_form is ViewBooksForm)
            {
                ViewBooksForm f = _form as ViewBooksForm;
                ColumnSelector selector = new ColumnSelector(f.DataGrid, _columns);
                selector.ShowDialog();
            }
            _setupDialog.ShowDialog();
        }

        private void printDataGrid(DataGridView grid)
        {
            Graphics g = _printEvent.Graphics;
            //for the first page, print the header text
            if (_pageNumber == 1)
            {
                _x = _left;
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (!col.Visible || (_columns.Count > 0 && !_columns.Contains(col.Index)))
                        continue;
                    RectangleF r = new RectangleF(_x, _top + 60, col.Width, col.HeaderCell.Size.Height);
                    g.DrawString(col.HeaderText,_boldFont,_brush,r,_format);
                    _x += col.Width;
                }
                _x = _left;
                _y = _top + 80;
            }

            for (int i = _record; i < grid.Rows.Count; i++)
            {
                DataGridViewRow row = grid.Rows[i];
                _record++;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.Visible || (_columns.Count > 0 && !_columns.Contains(cell.ColumnIndex)))
                        continue;
                    RectangleF r = new RectangleF(_x, _y, cell.Size.Width, cell.Size.Height);
                    g.DrawString(cell.Value.ToString(), _regularFont, _brush, r, _format);
                    _x += cell.Size.Width;
                    if (_y >= _bottom - 100)
                    {
                        _printEvent.HasMorePages = true;
                        _y = _top + 60;
                        _x = _left;
                        return;
                    }
                    
                }
                _y += grid.Rows[0].Height;
                _x = _left;
            }

            _y = _top + 60;
            _x = _left;
        }

        private void printDomainObject(BaseDO domain)
        {
            Graphics g = _printEvent.Graphics;
            if (_pageNumber == 1)
            {
                _y = _top + 60;
                _x = _left;
            }
            
            if (domain is Book)
            {
                Book b = domain as Book;
                int height = 20;
                int i = 0;
                this.printFieldOnLine(g, "Short Title: ", b.ShortTitle, height);
                this.printFieldOnLine(g, "Long Title: ", b.LongTitle, height);
                this.printTwoFieldsOnLine(g, "ISBN: ", "Publisher: ", b.ISBN, b.PublisherInfo, height);
                for (i = 0; i < b.Authors.Count; i++)
                {
                    if (i == 0)
                        this.printFieldOnLine(g, "Authors: ", b.Authors[i].FullName,height);
                    else
                        this.printFieldOnLine(g, "", b.Authors[i].FullName, height);
                }

                for (i = 0; i < b.Editors.Count; i++)
                {
                    if (i == 0)
                        this.printFieldOnLine(g, "Editors: ", b.Editors[i].FullName, height);
                    else
                        this.printFieldOnLine(g, "", b.Editors[i].FullName, height);
                }

                for (i = 0; i < b.Subjects.Count; i++)
                {
                    if (i == 0)
                        this.printFieldOnLine(g, "Subjects: ", b.Subjects[i].Name, height);
                    else
                        this.printFieldOnLine(g, "", b.Subjects[i].Name, height);
                }

                this.printTwoFieldsOnLine(g, "Dewey Decimal: ", "Dewey Normalized: ", b.Dewey, b.DeweyNormalized, height);
                this.printTwoFieldsOnLine(g, "Edition: ","Physical Description: ", b.Edition, b.PhysicalDescription, 2 * height);
                this.printTwoFieldsOnLine(g, "Lib. of Congr.:", "Language: ", b.LibraryOfCongress, b.Language, height);
                this.printFieldOnLine(g, "Related URLs: ", b.Urls, b.Urls.Length / 4);
                this.printFieldOnLine(g, "Awards: ", b.Awards, b.Awards.Length / 4);
                this.printFieldOnLine(g, "Notes: ", b.Notes, b.Notes.Length / 4);
                this.printFieldOnLine(g, "Summary: ", b.Summary, b.Summary.Length / 4);

            }
        }

        private void printFieldOnLine(Graphics g, string caption, string value, int height)
        {
            if(value.Equals(""))
                return;
            
            //we let the graphics object figure out how big the line has to be before printing it
            SizeF size = g.MeasureString(value, _regularFont, _right - ((int)_x + 100));
            RectangleF r = new RectangleF(_x + 100, _y, size.Width, size.Height);
            RectangleF r2 = new RectangleF(_x, _y - 1, _x + 100, size.Height);
            g.DrawString(caption, _boldFont, _brush, r2, _wrap);
            g.DrawString(value, _regularFont, _brush, r, _wrap);
            
            //add 5 for padding, completely arbitrary
            _y += size.Height + 5;
        }

        private void printTwoFieldsOnLine(Graphics g, string caption1, string caption2, string value1, string value2, int height)
        {
            if (!value1.Equals(""))
            {
                RectangleF r = new RectangleF(_x + 100, _y, _right / 2, height);
                RectangleF r2 = new RectangleF(_x, _y - 1, _x + 100, height);
                g.DrawString(caption1, _boldFont, _brush, r2, _wrap);
                g.DrawString(value1, _regularFont, _brush, r, _wrap);
                _x = _right / 2;
            }
            else
            {
                _x = _left;
            }

            if (value2.Equals(""))
            {
                if(!value2.Equals(""))
                    _y += height;
                _x = _left;
                return;
            }
                
            RectangleF r3 = new RectangleF(_x + 100, _y, _right - (_x + 100), height);
            RectangleF r4 = new RectangleF(_x, _y - 1, _x + 100, height);
            g.DrawString(caption2, _boldFont, _brush, r4, _wrap);
            g.DrawString(value2, _regularFont, _brush, r3, _wrap);
            _y += height;
            _x = _left;
        }

        private void printHeader()
        {
            Graphics g = _printEvent.Graphics;
            Rectangle r = new Rectangle(_left, _top, _right - _left, 60);
            g.DrawString("Libellus Personal Library 1.0", _headerFont, _brush, r, _format);
            g.DrawString(this.getRightHeader(), _headerFont, _brush, r, _rightAlign);
            Pen p = new Pen(_brush);
            g.DrawLine(p, _left, _top + 45, _right, _top + 45);
        }

        private string getRightHeader()
        {
            if (_form is ViewBooksForm)
            {
                ViewBooksForm f = _form as ViewBooksForm;
                MainForm parent = f.MdiParent as MainForm;
                string owner = Utilities.Utils.GetPossessive(parent.DBInfo.Owner);
                string result = null;
                switch (f.LibraryMode)
                {
                    case Libellus.Utilities.Constants.LibraryMode.LIBRARY:
                        result = owner + " Library";
                        break;

                    case Libellus.Utilities.Constants.LibraryMode.LOANEDBOOKS:
                        result = owner + " Loaned Books";
                        break;

                    case Libellus.Utilities.Constants.LibraryMode.WISHLIST:
                        result = owner + " Wish List";
                        break;

                    case Libellus.Utilities.Constants.LibraryMode.LOANHISTORY:
                        result = owner + " Loan History";
                        break;

                    case Libellus.Utilities.Constants.LibraryMode.AVAILABLEBOOKS:
                        result = owner + " Available Books";
                        break;

                    default:
                        return "";
                }

                return result;
            }
            else if (_form is AddBookForm)
            {
                AddBookForm f = _form as AddBookForm;
                return "Details on " + f.BookData.ShortTitle;
            }
            else
                return "";
        }

        private void printFooter()
        {
            Graphics g = _printEvent.Graphics;
            Rectangle r = new Rectangle(_left, _bottom - 20, _right - _left, 40);
            Pen p = new Pen(_brush);
            g.DrawLine(p, _left, _bottom - 45, _right, _bottom - 45);
            g.DrawString(DateTime.Now.ToLongDateString(), _headerFont, _brush, r, _format);
            g.DrawString("Page " + _pageNumber, _headerFont, _brush, r, _rightAlign);

            if (_printEvent.HasMorePages)
            {
                _pageNumber++;
            }
            else
            {
                _pageNumber = 1;
                _record = 0;
            }
        }
    }
}
