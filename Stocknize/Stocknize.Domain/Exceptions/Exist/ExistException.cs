using System;

namespace Stocknize.Domain.Exceptions
{
    public class ExistException : Exception
    {
        public ExistException(string message) : base(message)
        {
        }
    }
}
