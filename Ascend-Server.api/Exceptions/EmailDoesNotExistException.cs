using System; 
using Models; 

namespace Exceptions; 

public class EmailDoesNotExistException : Exception
{
     private readonly string _email;

        public override string Message => "Email: " + _email + " could not be found in our system. Please create an account first.";

        public EmailDoesNotExistException(string? email)
        {
            _email = email ?? "";
        }
        
}