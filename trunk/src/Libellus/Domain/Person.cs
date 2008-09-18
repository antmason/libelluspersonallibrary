using System;
using System.Collections.Generic;
using System.Text;
using Libellus.XML;
using Libellus.Utilities;

namespace Libellus.Domain
{
    public class Person
    {
        private string _firstName;
        private string _lastName;
        private string _id;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FullName
        {
            get { return _lastName + ", " + _firstName; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// we make a wonderfully vain attempt to determine what the user is thinking: last_name, firstname or firstname lastname
        /// and so we split it by checking for a comma and assuming.  B/c we know what assuming does.  
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string GetLastNameFromFull(string fullName)
        {
            int comma = fullName.IndexOf(',');
            int len = fullName.Length;
            int lastSpace = fullName.LastIndexOf(" ");

            if (fullName.Contains(","))
            {
                try
                {
                    return fullName.Substring(0, comma).Trim();
                }
                catch (Exception e)
                {
                    ExceptionHandler.HandleException(e);
                    return "";
                }
            }
            else
            {
                if (fullName.Contains(" "))
                {
                    return fullName.Substring(lastSpace).Trim();
                }
                else
                {
                    //well if the string doesn't contain any spaces, or commas...we're gonna assume its somebody with one name,
                    //like "Prince" or "Madonna" and they deserve to have their one word name as their last name.
                    return fullName.Trim();
                }
            }
        }

        public static string GetFirstNameFromFull(string fullName)
        {
            int comma = fullName.IndexOf(',');
            int len = fullName.Length;
            int lastSpace = fullName.LastIndexOf(" ");

            if (fullName.Contains(","))
            {
                try
                {
                    return fullName.Substring(comma + 1).Trim();
                }
                catch (Exception e)
                {
                    ExceptionHandler.HandleException(e);
                }

                return "";

            }
            else
            {
                if (fullName.Contains(" "))
                {
                    try
                    {
                        return fullName.Substring(0, lastSpace).Trim();
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.HandleException(e);
                    }

                    return "";

                }
                else
                {
                    //if there is only one name, we assume its the last name and return nothing for the first name
                    //b/c we're evil like that.
                    return "";
                }
            }
        }

        public void FillFromElement(Element e)
        {
            this.FirstName = Person.GetFirstNameFromFull(e.Value);
            this.LastName = Person.GetLastNameFromFull(e.Value);
            this.Id = e.GetAttribute("person_id");
        }

        public static string GetLastNameFromPersonValue(string fullname)
        {
            string full = fullname.Trim();
            int lastSpace = full.Trim().LastIndexOf(" ");
            string lastName = full.Substring(lastSpace).Trim();
            if (lastName[lastName.Length - 1] == ',')
                lastName = lastName.Substring(0,lastName.Length - 2);
            return lastName.Trim();
        }

        public static string GetFirstNameFromPersonValue(string fullname)
        {
            string full = fullname.Trim();
            int lastSpace = full.LastIndexOf(" ");
            string firstName = full.Substring(0, lastSpace).Trim();
            return firstName;
        }
    }
}
