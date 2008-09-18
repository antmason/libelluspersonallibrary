using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Utilities
{
    public class DDItem
    {

       private string _key = "";
       private string _val = "";
       public DDItem(string key, string value)
       {
           _key = key;
           _val = value;
       }
       public string Value
       {
           get { return _val; }
           set { _val = value; }
       }

       public string Key
       {
           get { return _key; }
           set { _key = value; }
       }
       public override string ToString()
       {
           return _key;
       }

    }
}
