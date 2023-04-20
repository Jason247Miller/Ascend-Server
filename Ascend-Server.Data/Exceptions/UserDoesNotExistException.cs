
namespace Data.Exceptions;

public class UserDoesNotExistException : Exception
{
    public override string Message => "Error: Invalid User Id";

}