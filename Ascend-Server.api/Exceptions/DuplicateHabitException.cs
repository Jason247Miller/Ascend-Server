using System;
namespace Exceptions;

public class DuplicateHabitException : Exception
{
    public override string Message => "A Habit containing the passed Id Already Exists.";

}