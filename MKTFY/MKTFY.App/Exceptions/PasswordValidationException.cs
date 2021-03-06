using System;

namespace MKTFY.App.Exceptions
{
    public class PasswordValidationException : Exception
    {       
        public PasswordValidationException() { }

        public PasswordValidationException(string message) : base(message)
        {

        }
    }
}