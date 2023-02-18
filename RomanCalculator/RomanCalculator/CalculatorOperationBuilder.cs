using System.Collections.Generic;
using System.Linq;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Operations.Common;
using RomanCalculator.Operations;

namespace RomanCalculator
{
    public class CalculatorOperationBuilder : ICalculatorOperationBuilder
    {
        private readonly List<CalculatorOperation> _calculatorOperations = new List<CalculatorOperation>();
        protected IReadOnlyCollection<CalculatorOperation> CalculatorOperations => _calculatorOperations;

        public CalculatorOperationBuilder()
        {

        }

        public static ICalculatorOperationBuilder CreateDefault()
        {
            var calculatorBuilder = new CalculatorOperationBuilder()
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
            AddMultiplicationOperation();
            AddDivisionOperation();
            AddAdditionOperation();
            AddSubstractionOperation();

            return this;
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

        public IOrderedEnumerable<IGrouping<int, CalculatorOperation>> Build()
        {
            return CalculatorOperations
                .GroupBy(calculatorOperation => calculatorOperation.ExecutePriority)
                .OrderBy(calculatorOperation => calculatorOperation.Key);
        }
    }
}
