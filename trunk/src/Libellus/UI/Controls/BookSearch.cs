using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Libellus.Utilities;

namespace Libellus.UI.Controls
{
    public partial class BookSearch : UserControl
    {
        private BookGrid _grid;
        
        public BookSearch()
        {
            InitializeComponent();
            this.cmbSearchBy.Items.Add(new Item("", Constants.SearchMode.NONE));
            this.cmbSearchBy.Items.Add(new Item("ISBN",Constants.SearchMode.ISBN));
            this.cmbSearchBy.Items.Add(new Item("Title", Constants.SearchMode.TITLE));
            this.cmbSearchBy.Items.Add(new Item("Author Last Name", Constants.SearchMode.AUTHOR_LAST));
            this.cmbSearchBy.Items.Add(new Item("Author First Name", Constants.SearchMode.AUTHOR_FIRST));
            this.cmbSearchBy.Items.Add(new Item("Subject", Constants.SearchMode.SUBJECT));
            this.cmbSearchBy.Items.Add(new Item("Publisher", Constants.SearchMode.PUBLISHER));
        }

        public BookGrid BookGrid
        {
            get { return _grid; }
            set { _grid = value; }
        }

        public Button SearchButton
        {
            get { return this.btnSearch; }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Item i = this.cmbSearchBy.SelectedItem as Item;
            Constants.SearchMode value;
            if (i == null)
                value = Constants.SearchMode.NONE;
            else
                value = i.Value;
            this._grid.Search(value, this.txtSearchFor.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.cmbSearchBy.SelectedIndex = 0;
            this.txtSearchFor.Text = "";
        }
    }

    class Item
    {
        private string _name;
        private Constants.SearchMode _value;
        public Item(string name, Constants.SearchMode value)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }

        public Constants.SearchMode Value
        {
            get { return _value; }
        }
    }
}
