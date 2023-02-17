using RomanCalculator.Core.Operations.Common;

namespace RomanCalculator.Operations
{
    /// <summary>
    /// Операция вычитания
    /// </summary>
    internal sealed class SubstractionCalculatorOperation : CalculatorOperation
    {
        public SubstractionCalculatorOperation(int executePriority, char operationMark) 
            : base(executePriority, operationMark)
        {
        }

        public override decimal Evaluate(string input) 
            => LeftOperand - RightOperand;
    }
}
