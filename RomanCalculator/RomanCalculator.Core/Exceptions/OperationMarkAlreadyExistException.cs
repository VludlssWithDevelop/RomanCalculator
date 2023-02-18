using System;

namespace RomanCalculator.Core.Exceptions
{
    public class OperationMarkAlreadyExistException : Exception
    {
        public OperationMarkAlreadyExistException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
