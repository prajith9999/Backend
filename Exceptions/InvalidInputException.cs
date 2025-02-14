using System;

namespace LandWind.Exceptions
{
    // Custom exception class for invalid input scenarios
    public class InvalidInputException : Exception
    {
        // Constructor that accepts a custom message
        public InvalidInputException(string message) : base(message)
        {
        }

        // Constructor that accepts a custom message and an inner exception
        public InvalidInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
