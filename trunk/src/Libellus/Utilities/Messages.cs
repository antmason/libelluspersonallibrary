using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Utilities
{
    class Messages
    {
        public struct Common
        {
            public static string DB_DELETED = "The library you selected has been successfully deleted.";
        }
        public struct NewDBForm
        {
            public static string DB_CREATED = "Your new library has been successfully created, would you like to view it now?";    
        }

        public struct ViewDBForm
        {
            public static string DB_DELETE_CONFIRM = "Are you positive you wish to delete this library, and all information within it?  Once deleted, this information cannot be recovered.";
        }
    }
}
