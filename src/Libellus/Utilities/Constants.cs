using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Utilities
{
    public class Constants
    {
        private static string WEBSERVICE_URI = "http://isbndb.com/api/books.xml?access_key=UGRZZGHT";
        public static string DATETIMEFORMAT = "m/d/yy h:mm";

        public static string GetWebServiceURI(string mode, string val)
        { 
            string result = Constants.WEBSERVICE_URI;
            result += "&results=texts&results=details&results=authors&results=prices&results=subjects&index1=" + mode + "&value1=" + val;
            return result;
        }

        public enum XMLResultReturnValue
        {
            SUCCESS,
            BOOK_ALREADY_EXISTS,
            NOT_FOUND,
            UNKNOWN
        }

        public enum LibraryMode
        {
            LIBRARY,
            WISHLIST,
            LOANEDBOOKS,
            AVAILABLEBOOKS,
            LOANHISTORY
        }

        public enum SearchMode
        {
            AUTHOR_LAST,
            AUTHOR_FIRST,
            TITLE,
            ISBN,
            SUBJECT,
            PUBLISHER,
            BOOK_ID,
            NONE,
        }
        
        public enum AddBookMode
        {
            ADD,
            VIEWONLY,
            EDIT
        }
        
        public struct Settings
        {
        	public static string DEFAULT_DB = "default_db";
        }
    }
}
