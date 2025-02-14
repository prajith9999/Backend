using System;

namespace LandWind.Exceptions
{
    // Custom exception class for resource not found scenarios
    public class NotFoundException : Exception
    {
        // Constructor that accepts a custom message
        public NotFoundException(string message) : base(message)
        {
        }

        // Constructor that accepts a custom message and an inner exception
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
