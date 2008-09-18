using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using Libellus.UI.Forms;

namespace Libellus.Utilities
{
    class ExceptionHandler
    {
        public static void HandleException(Exception e)
        {
            if (e is SQLiteException)
            {
                if (e.Message.Contains("constraint"))
                    return;
            }

            ExceptionWindow window = new ExceptionWindow(e);
            window.ShowDialog();
        }
    }
}
