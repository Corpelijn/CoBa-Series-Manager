using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Exceptions
{
    class IllegalArgumentException : Exception
    {
        public IllegalArgumentException(string message)
            : base(message)
        {
        }
    }
}
