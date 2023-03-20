using System;
using Models; 
namespace Exceptions;

    public class UserDoesNotExistException : Exception
    {
        private readonly int _userId;

        public override string Message => $"Error: No User with Id '{_userId}' Exists.";

        public UserDoesNotExistException(int userId)
        {
            _userId = userId;
        }
    }