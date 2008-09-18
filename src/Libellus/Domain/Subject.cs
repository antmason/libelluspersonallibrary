using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Domain
{
    public class Subject : BaseDO
    {
        private string _id;
        private string _subject;

        public string Id
        {
            get { return CheckNull(_id); }
            set { _id = value; }
        }

        public string Name
        {
            get { return CheckNull(_subject); }
            set { _subject = value; }
        }
    }
}
