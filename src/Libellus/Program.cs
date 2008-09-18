using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Libellus.UI.Forms;
using Libellus.Utilities;

namespace Libellus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
                return;
            }
            
        }
    }
}
