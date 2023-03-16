using System;

namespace Exceptions;

    public class JournalEntryNotFoundException : Exception
    {

        public override string Message => "Error: Guided Journal Entry is Invalid, or has been deleted.";

    }
