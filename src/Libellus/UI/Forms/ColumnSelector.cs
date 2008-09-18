using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Libellus.UI.Forms
{
    public partial class ColumnSelector : Libellus.UI.Forms.BaseForm
    {
        private DataGridView _grid;
        private List<int> _list;

        public ColumnSelector(DataGridView grid,List<int> columns)
        {
            InitializeComponent();
            _grid = grid;
            _list = columns;
            int count = 0;
            int x = 50;
            int y = 25;
            foreach (DataGridViewColumn col in _grid.Columns)
            {
                if (col.Visible == false)
                    continue;

                CheckBox box = new CheckBox();
                box.Name = "chkbox" + count;
                box.Location = new Point(x,y);


                int index = col.Index;

                if (columns.Contains(index))
                    box.Checked = true;

                box.Text = col.HeaderText;
                box.Width = 120;
                y += 20;
                this.groupBox1.Controls.Add(box);
                count++;
            }

            this.groupBox1.Height = this.groupBox1.Controls[this.groupBox1.Controls.Count - 1].Location.Y + 50;
            this.btnSubmit.Location = new Point(this.btnSubmit.Location.X,this.groupBox1.Height + 30);
            this.Height = this.groupBox1.Height + 100;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int count = 0;
            _list.Clear();
            foreach (DataGridViewColumn col in _grid.Columns)
            {
                if (col.Visible == false)
                    continue;
                CheckBox box = this.groupBox1.Controls.Find("chkbox" + count, true)[0] as CheckBox;
                
                if (box != null && box.Checked)
                    _list.Add(col.Index);
                count++;
            }

            this.Close();
        }


    }
}

