using System;

namespace Exceptions;

public class DuplicateEntryException : Exception
{
    public override string Message => "A Journal Entry containing the passed Id Already Exists.";

}