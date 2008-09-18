using System;
using System.Collections.Generic;
using System.Text;

namespace Libellus.Domain
{
    public class BaseDO
    {
        protected string CheckNull(string input)
        {
            return input == null ? "" : input;
        }
    }
}
