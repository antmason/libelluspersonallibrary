using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Utilities
{
    /// <summary>
    /// Class just to have one point of maintenance for error messages
    /// </summary>
    class ErrorMessages
    {

        public struct Common
        {
            public static string REQUIRED_FIELD = "You must enter this field to move forward";
            public static string INCORRECT_PASSWORD = "The password you provided was incorrect, please try again";
            public static string COULD_NOT_DELETE_DB = "The database you selected could not be deleted.  Your account probably does not have the proper privileges.";
            public static string WEB_SERVICE_ERROR = "The online database that holds book information is not responding.  Please check that your internet connection is available, or perhaps try again later.";
            public static string BOOK_EXISTS_IN_LIBRARY = "The book you entered is already in your library.";
            public static string CANT_OPEN_DB_CONNECTION = "Your library could not be opened.  Check if another instance of the application is running or if your application folder / files have been moved.";
            public static string BOOK_NOT_FOUND = "The ISBN you entered was not found.";
        }

        public struct NewDBForm
        {
            public static string PASSWORD = "You must enter a password, or click the checkbox to turn off password protection";
            public static string EMPTY_DB_MISSING = "The file 'emdata' is missing or corrupt, backup your libraries and reinstall the latest version of the application";
            public static string DB_ALREADY_EXISTS = "There is already a library with the name you have chosen, please choose a new name, or delete the old library";
            public static string DB_NOT_CREATED = "An unknown error occurred, and the new library was not created.  If this occurs again, try reinstalling the application";
        }

        public struct ViewDBForm
        {
            public static string DATA_DIR_NOT_FOUND = "The data directory has been deleted, moved, or corrupted.  You should reinstall the application";
            public static string DB_NOT_SELECTED = "You must select a library to open.";
        }
    }
}
