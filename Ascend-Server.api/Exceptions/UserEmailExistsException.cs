using System; 
using Models; 

namespace Exceptions; 

public class UserEmailExistsException : Exception
{
     private readonly User _user;

        public override string Message => $"User with the Email '{_user.Email}' already exists.";

        public UserEmailExistsException(User user)
        {
            _user = user;
        }
}