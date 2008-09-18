using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.XML
{
    public class Attribute
    {
        private string _name = "";
        private string _value = "";

        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public String Value
        {
            set { _value = value; }
            get { return _value; }
        }
    }
}
