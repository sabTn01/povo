using System;

namespace POVO.Backend.Infrastructure.Exceptions
{
    public class AuthentificationFailedException : Exception
    {
        public AuthentificationFailedException(string message) : base(message)
        {
        }
    }
}