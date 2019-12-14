using System;

namespace DevFactoryZ.CharityCRM
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
