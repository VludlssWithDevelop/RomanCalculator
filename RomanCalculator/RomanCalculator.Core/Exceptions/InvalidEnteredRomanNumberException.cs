using System;

namespace RomanCalculator.Core.Exceptions
{
    /// <summary>
    /// Введено не валидное римское число
    /// </summary>
    public class InvalidEnteredRomanNumberException : Exception
    {
        /// <summary>
        /// Не валидное римское число
        /// </summary>
        public string InvalidRomanNumber { get; }

        public InvalidEnteredRomanNumberException(string romanNumber)
        {
            InvalidRomanNumber = romanNumber;
        }
    }
}
