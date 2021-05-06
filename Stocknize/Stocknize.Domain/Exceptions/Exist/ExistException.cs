using System;

namespace Stocknize.Domain.Exceptions.Exist
{
    internal class ExistException : Exception
    {
        public ExistException(string message) : base(message)
        {
        }
    }
}
