using System;

namespace Exceptions;

    public class NotFoundException : Exception
    {
        private readonly object _item;

        public override string Message => $"{_item}' was not found in the database.";

        public NotFoundException(object item)
        {
            _item = item;
        }
    }
