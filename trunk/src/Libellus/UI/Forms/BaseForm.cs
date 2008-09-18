using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Libellus.UI.Forms
{
    /// <summary>
    /// This form is here simply for inheritance, its not to be instantiated
    /// directly.  This helps eliminate redundant code
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// Generated Constructor
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);   
        }
    }
}
