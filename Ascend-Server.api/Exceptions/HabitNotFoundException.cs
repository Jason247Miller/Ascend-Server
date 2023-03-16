using System;

namespace Exceptions;

    public class HabitNotFoundException : Exception
    {

        public override string Message => "Error: Habit is Invalid, or has been deleted.";

    }
