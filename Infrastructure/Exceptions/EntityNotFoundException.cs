using System;

namespace POVO.Backend.Infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type type) : base($"{type.Name} not found")
        {
        }

        public EntityNotFoundException(Type type, Guid guid) : base($"{type.Name} not found with GUID {guid}")
        {
        }

        public EntityNotFoundException(Type type, int id) : base($"{type.Name} not found with ID {id}")
        {
        }

        public EntityNotFoundException(Type type, string prop, string value) : base($"{type.Name} not found with prop «{prop}»: {value}")
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}