using System;
using Models;

namespace Exceptions;

public class UserDoesNotExistException : Exception
{

    public override string Message => "Error: Invalid User Id";

}