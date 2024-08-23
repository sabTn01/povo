using System;

namespace POVO.Backend.Infrastructure.Exceptions
{
    public class PermissionFailedException : Exception
    {
        public PermissionFailedException(string message) : base(message)
        {
        }
    }
}