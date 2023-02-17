using RomanCalculator.Core.Operations.Common;

namespace RomanCalculator.Operations
{
    /// <summary>
    /// Операция деления
    /// </summary>
    internal sealed class DivisionCalculatorOperation : CalculatorOperation
    {
        public DivisionCalculatorOperation(int executePriority, char operationMark) 
            : base(executePriority, operationMark)
        {
        }

        public override decimal Evaluate(string input) 
            => LeftOperand / RightOperand;
    }
}
