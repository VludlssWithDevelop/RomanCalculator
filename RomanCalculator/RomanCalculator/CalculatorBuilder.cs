using System.Collections.Generic;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Operations.Common;

namespace RomanCalculator
{
    public class CalculatorBuilder : ICalculatorOperationBuilder
    {
        private readonly List<CalculatorOperation> _calculatorOperations = new List<CalculatorOperation>();
        public IReadOnlyCollection<CalculatorOperation> CalculatorOperations => _calculatorOperations;

        public CalculatorBuilder()
        {

        }

        public ICalculatorOperationBuilder AddOperation(CalculatorOperation operation)
        {
            _calculatorOperations.Add(operation);
            return this;
        }
    }
}
