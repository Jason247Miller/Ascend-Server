using System;

namespace Exceptions;

    public class HabitNotFoundException : Exception
    {

        public override string Message => "Habit Not Found";

    }
