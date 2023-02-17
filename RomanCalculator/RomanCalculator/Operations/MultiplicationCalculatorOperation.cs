using RomanCalculator.Core.Operations.Common;

namespace RomanCalculator.Operations
{
    /// <summary>
    /// Операция умножения
    /// </summary>
    internal sealed class MultiplicationCalculatorOperation : CalculatorOperation
    {
        public MultiplicationCalculatorOperation(int executePriority, char operationMark) 
            : base(executePriority, operationMark)
        {
        }

        public override decimal Evaluate(string input) 
            => LeftOperand * RightOperand;
    }
}
