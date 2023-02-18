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
                .AddMultiplicationOperation()
                .AddDivisionOperation()
                .AddAdditionOperation()
                .AddSubstractionOperation();

            return calculatorBuilder;
        }

        public ICalculatorOperationBuilder AddOperation(CalculatorOperation operation)
        {
            _calculatorOperations.Add(operation);
            return this;
        }

        public ICalculatorOperationBuilder AddDefaultOperations()
        {
            var calculatorBuilder = this.AddMultiplicationOperation()
                .AddDivisionOperation()
                .AddAdditionOperation()
                .AddSubstractionOperation();

            return calculatorBuilder;
        }

        public ICalculatorOperationBuilder AddAdditionOperation(int executePriority = 2, char operationMark = '+')
        {
            _calculatorOperations.Add(new AdditionCalculatorOperation(executePriority, operationMark));
            return this;
        }

        public ICalculatorOperationBuilder AddDivisionOperation(int executePriority = 1, char operationMark = '/')
        {
            _calculatorOperations.Add(new DivisionCalculatorOperation(executePriority, operationMark));
            return this;
        }

        public ICalculatorOperationBuilder AddMultiplicationOperation(int executePriority = 1, char operationMark = '*')
        {
            _calculatorOperations.Add(new MultiplicationCalculatorOperation(executePriority, operationMark));
            return this;
        }

        public ICalculatorOperationBuilder AddSubstractionOperation(int executePriority = 2, char operationMark = '-')
        {
            _calculatorOperations.Add(new SubstractionCalculatorOperation(executePriority, operationMark));
            return this;
        }
    }
}
