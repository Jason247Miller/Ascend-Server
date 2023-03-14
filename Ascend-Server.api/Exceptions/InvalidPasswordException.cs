using System;

namespace Exceptions;

    public class InvalidPasswordException : Exception
    {
        private readonly object _email;

        public override string Message => "Error: Password for " + _email + " is Invalid.";

        public InvalidPasswordException(string email)
        {
            _email = email;
        }
    }
