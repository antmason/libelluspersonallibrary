using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Domain
{
    public class BaseInfo : BaseDO
    {
        private string _dbname;
        private string _owner;
        private string _password;
        private string _dateCreated;
        private string _lastAccessed;

        public string DBName
        {
            get { return CheckNull(_dbname); }
            set { _dbname = value; }
        }


        public string Owner
        {
            get { return CheckNull(_owner); }
            set { _owner = value; }
        }

        public string Password 
        {
            get { return CheckNull(_password); }
            set { _password = value; }
        }

        public string DateCreated
        {
            get { return CheckNull(_dateCreated); }
            set { _dateCreated = value; }
        }

        public string LastAccessed
        {
            get { return CheckNull(_lastAccessed); }
            set { _lastAccessed = value; }
        }

    }
}
