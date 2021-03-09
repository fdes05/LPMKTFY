using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.App.Exceptions
{
    public class EmailVerificationException : Exception
    {
        public EmailVerificationException() { }

        public EmailVerificationException(string message) : base(message)
        {

        }
    }
}
