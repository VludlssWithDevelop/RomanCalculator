using System;
using System.Text.RegularExpressions;
using RomanCalculator.Core.Exceptions;
using RomanNumerals;

namespace RomanCalculator.Core.Operations.Common
{
    /// <summary>
    /// Базовый класс всех операций
    /// </summary>
    public abstract class CalculatorOperation
    {
        private const string LeftOperandSectionKey = "LeftOperandSection";
        private const string RightOperandSectionKey = "RightOperandSection";

        private const string LeftOperandMinusKey = "LeftOperandMinus";
        private const string RightOperandMinusKey = "RightOperandMinus";

        private const string LeftOperandValueKey = "LeftOperandValue";
        private const string RightOperandValueKey = "RightOperandValue";

        protected Regex[] Patterns { get; set; }
        protected decimal LeftOperand { get; set; }
        protected decimal RightOperand { get; set; }

        public int ExecutePriority { get; }
        public string MatchedPattern { get; set; }

        public CalculatorOperation(int executePriority, char operationMark)
        {
            ExecutePriority = executePriority;

            var escapedOperationMark = Regex.Escape(char.ToString(operationMark));

            var romanNumberPattern = $@"(?<{LeftOperandSectionKey}>(?<{LeftOperandMinusKey}>\-?)(?<{LeftOperandValueKey}>[{CalculatorConstants.ValidRomanNumberChars}]+((\.|\,)[{CalculatorConstants.ValidRomanNumberChars}]+)?)){escapedOperationMark}(?<{RightOperandSectionKey}>(?<{RightOperandMinusKey}>\-?)(?<{RightOperandValueKey}>[{CalculatorConstants.ValidRomanNumberChars}]+((\.|\,)[{CalculatorConstants.ValidRomanNumberChars}]+)?))";
            var romanNumberParenthesesPattern = $@"\({romanNumberPattern}\)";

            // Порядок очень важен, надо чтобы первыми шли шаблоны со скобками и с дробными числами, чтобы приоритет выполнения операций был правильный
            Patterns = new Regex[]
            {
                new Regex(romanNumberParenthesesPattern),
                new Regex(romanNumberPattern),
            };
        }

        public bool IsMatch(string input)
        {
            const int matchedPatternIndex = 0;

            for (int patternIndex = default; patternIndex < Patterns.Length; patternIndex++)
            {
                if (!Patterns[patternIndex].IsMatch(input))
                    continue;

                const char dotChar = '.';
                const char commaChar = ',';

                var matchedValues = Patterns[patternIndex].Match(input).Groups;

                var leftOperand = matchedValues[LeftOperandValueKey].Value.Replace(dotChar, commaChar);
                var isLeftOperandNegative = matchedValues[LeftOperandMinusKey].Value != string.Empty;

                var rightOperand = matchedValues[RightOperandValueKey].Value.Replace(dotChar, commaChar);
                var isRightOperandNegative = matchedValues[RightOperandMinusKey].Value != string.Empty;

                MatchedPattern = matchedValues[matchedPatternIndex].Value;
                LeftOperand = GetDecimalNumberFromRomanNumber(leftOperand, isLeftOperandNegative);
                RightOperand = GetDecimalNumberFromRomanNumber(rightOperand, isRightOperandNegative);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Получить десятичное число (в том числе с плавающей точкой) из строки, содержащей римское число
        /// </summary>
        /// <param name="romanNumber">Римское число</param>
        /// <returns>Десятичное число</returns>
        /// <exception cref="NotValidRomanNumber">Если римское число не настоящее</exception>
        private decimal GetDecimalNumberFromRomanNumber(string romanNumber, bool isNegativeNumber)
        {
            const int leftPartIndex = 0;
            const int rightPartIndex = 1;

            var isNumberFloat = romanNumber.Contains(",");

            if (isNumberFloat)
            {
                var leftOperandInnerParts = romanNumber.Split(',');

                if (!RomanNumeral.TryParse(leftOperandInnerParts[leftPartIndex], out int leftPart))
                    throw new InvalidEnteredRomanNumberException(leftOperandInnerParts[leftPartIndex]);

                if (!RomanNumeral.TryParse(leftOperandInnerParts[rightPartIndex], out int rightPart))
                    throw new InvalidEnteredRomanNumberException(leftOperandInnerParts[rightPartIndex]);

                var result = Convert.ToDecimal($"{leftPart},{rightPart}");

                return isNegativeNumber ? -result : result;
            }

            if (!RomanNumeral.TryParse(romanNumber, out int number))
                throw new InvalidEnteredRomanNumberException(romanNumber);

            return isNegativeNumber ? -number : number;
        }

        public abstract decimal Evaluate(string input);
    }
}
