using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Libellus.Utilities
{
    /// <summary>
    /// Generic class for useful utility functions
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Receives a string with parameter spots, in syntax of {0},{1},{2}... and replaces each {#} with
        /// the value in that array location.  This is just used to avoid concatention of ungodly numbers
        /// of strings when making sql statements
        /// </summary>
        /// <param name="original"></param>
        /// <param name="replacements"></param>
        /// <returns></returns>
        public static string ReplaceSQLParameters(string original, object[] replacements)
        {
            for(int i = 0; i<replacements.Length; i++)
            {
                if (replacements[i] is int)
                    original = original.Replace("{" + i + "}", replacements[i].ToString());
                else if (replacements[i] is string)
                    original = original.Replace("{" + i + "}", '\'' + replacements[i].ToString() + '\'');
                else
                {
                    throw new Exception("SQL Parameter Not Supported: " + replacements[i].ToString());
                }
            }

            return original;

        }


        public static bool CheckISBN(string ISBN)
        {
            Regex validISBN = new Regex(@"\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)$");
            return validISBN.IsMatch(ISBN);
        }

        public static string GetPossessive(string name)
        {
            if (name.EndsWith("s"))
                return name + "'";
            else
                return name + "'s";
        }

        public static string CreateIdFromString(string s)
        {
            //we try our best to strip any special characters our by a series of replacements
            s = s.ToLower().Trim();
            s = s.Replace("'", "");
            s = s.Replace(",", "");
            s = s.Replace("(", "");
            s = s.Replace(")", "");
            s = s.Replace(".", "");
            s = s.Replace(" ", "_");
            return s;
        }
    }
}
