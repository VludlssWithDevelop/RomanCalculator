using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RomanCalculator.Core;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Exceptions;
using RomanCalculator.Core.Operations.Common;
using RomanNumerals;

namespace RomanCalculator
{
    public class Calculator : ICalculator
    {
        private readonly IOrderedEnumerable<IGrouping<int, CalculatorOperation>> _groupedCalculatorOperations;

        public Calculator(ICalculatorOperationBuilder calculatorBuilder)
        {
            _groupedCalculatorOperations = calculatorBuilder.Build();
        }

        public Calculator(Action<ICalculatorOperationBuilder> calculatorBuilderFunc)
        {
            var calculatorBuilder = new CalculatorOperationBuilder();

            calculatorBuilderFunc(calculatorBuilder);

            _groupedCalculatorOperations = calculatorBuilder.Build();
        }

        public static Calculator CreateDefault()
        {
            var calculatorBuilder = CalculatorOperationBuilder.CreateDefault();

            return new Calculator(calculatorBuilder);
        }

        public static Calculator Create(Action<ICalculatorOperationBuilder> calculatorBuilderFunc)
        {
            var calculatorBuilder = new CalculatorOperationBuilder();

            calculatorBuilderFunc(calculatorBuilder);

            return new Calculator(calculatorBuilder);
        }

        public string Evaluate(string input)
        {
            var formattedInput = Regex.Replace(input, @"\s+", string.Empty);

            if (!_groupedCalculatorOperations.Any())
                throw new InvalidCalculatorOperationsException();

            if (IsLastOneOperand(formattedInput))
                throw new InvalidMathExpressionException();

            while (true)
            {
                bool isAtLeastOneValidOperationMark = false;

                foreach (var calculatorOperationsGroup in _groupedCalculatorOperations)
                {
                    var calculatorOperationsQueue = new Queue<CalculatorOperation>(calculatorOperationsGroup);

                    while (calculatorOperationsQueue.Any())
                    {
                        CalculatorOperation highestPriorityOperation = calculatorOperationsQueue.Dequeue();

                        if (!highestPriorityOperation.IsMatch(formattedInput))
                            continue;

                        if (!isAtLeastOneValidOperationMark)
                            isAtLeastOneValidOperationMark = true;

                        var operationEvaluateResult = highestPriorityOperation.Evaluate(formattedInput);

                        bool isOperationEvaluateResultInteger = operationEvaluateResult == (int)operationEvaluateResult;

                        string resultAsString = null;

                        if (isOperationEvaluateResultInteger)
                        {
                            resultAsString = new RomanNumeral((int)operationEvaluateResult).ToString();
                        }
                        else
                        {
                            var splittedFloatNumber = operationEvaluateResult.ToString(".000")
                                .Split(',')
                                .Select(number => Convert.ToInt32(number))
                                .ToList();

                            resultAsString = $"{new RomanNumeral(splittedFloatNumber[0])},{new RomanNumeral(splittedFloatNumber[1])}";
                        }

                        formattedInput = formattedInput.Replace(highestPriorityOperation.MatchedPattern, resultAsString);
                    }
                }

                if (IsLastOneOperand(formattedInput))
                    return formattedInput;

                if (!isAtLeastOneValidOperationMark)
                    throw new InvalidCalculatorOperationMarkException();
            }
        }

        private bool IsLastOneOperand(string input)
            => Regex.IsMatch(input, $@"^\-?[{CalculatorConstants.ValidRomanNumberChars}]+((\.|\,)[{CalculatorConstants.ValidRomanNumberChars}]+)?$");
    }
}
