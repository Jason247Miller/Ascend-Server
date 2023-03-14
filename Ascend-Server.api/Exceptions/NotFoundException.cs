using System;

namespace Exceptions;

    public class NotFoundException : Exception
    {
        private readonly object _item;

        public override string Message => $"{_item}' does not exists.";

        public NotFoundException(object item)
        {
            _item = item;
        }
    }
