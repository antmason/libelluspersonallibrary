using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Libellus.Utilities
{
    class UIUtils
    {
        /// <summary>
        /// Most forms use errorprovider for validation, this method is a utility method that will
        /// control the toggling of the errorprovider, and it returns a 1 if there was an error,
        /// and 0 if there wasn't.  This is most often used with a int errorCount = 0; statement so
        /// the validation can determine if there were errors easily.
        /// </summary>
        /// <param name="test"></param>
        /// <param name="provider"></param>
        /// <param name="control"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int ValidationHelper(bool test, ErrorProvider provider, Control control, string msg)
        {
            if (test)
            {
                provider.SetError(control, msg);
                return 1;
            }
            else
            {
                provider.SetError(control, "");
                return 0;
            }
        }
    }
}
