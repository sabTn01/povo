using System;

namespace POVO.Backend.Infrastructure.Exceptions 
{ 
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, ExceptionName code = null) : base(message)
        {
            if (code == null)
            {
                code = ExceptionNameFactory.BadRequest.Generic;
            }
        }
    }
}