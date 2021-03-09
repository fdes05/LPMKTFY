using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.App.Exceptions
{
    public class UserNotSavedException : Exception
    {
        public UserNotSavedException() { }

        public UserNotSavedException(string message) : base(message)
        {

        }
    }
}
