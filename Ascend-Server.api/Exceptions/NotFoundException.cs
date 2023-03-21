using System;

namespace Exceptions;

    public class NotFoundException : Exception
    {
        private readonly string _entity;

        public override string Message => $"{_entity}' does not exists.";

        public NotFoundException(string entity)
        {
            _entity = entity;
        }
    }
