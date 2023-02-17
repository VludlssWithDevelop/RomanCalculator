using System.Collections.Generic;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Operations.Common;
using RomanCalculator.Operations;

namespace RomanCalculator
{
    public class CalculatorBuilder : ICalculatorOperationBuilder
    {
        private readonly List<CalculatorOperation> _calculatorOperations = new List<CalculatorOperation>();
        public IReadOnlyCollection<CalculatorOperation> CalculatorOperations => _calculatorOperations;

        public CalculatorBuilder()
        {

        }

        public static ICalculatorOperationBuilder CreateDefault()
        {
            var calculatorBuilder = new CalculatorBuilder()
                .AddOperation(new MultiplicationCalculatorOperation(executePriority: 1, operationMark: '*'))
                .AddOperation(new DivisionCalculatorOperation(executePriority: 1, operationMark: '/'))
                .AddOperation(new AdditionCalculatorOperation(executePriority: 2, operationMark: '+'))
                .AddOperation(new SubstractionCalculatorOperation(executePriority: 2, operationMark: '-'));

            return calculatorBuilder;
        }

        public ICalculatorOperationBuilder AddOperation(CalculatorOperation operation)
        {
            _calculatorOperations.Add(operation);
            return this;
        }
    }
}
