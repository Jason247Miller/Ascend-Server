using System;
using Models; 
namespace Exceptions;

    public class UserDoesNotExistException : Exception
    {
        private readonly int _userId;

        public override string Message => $"Error: User Id: '{_userId}' is Invalid.";

        public UserDoesNotExistException(int userId)
        {
            _userId = userId;
        }
    }