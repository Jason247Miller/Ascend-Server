
namespace Data.Exceptions;

public class UnauthorizedException : Exception
{
    public override string Message => "Authentication failed due to invalid credentials.";

}
