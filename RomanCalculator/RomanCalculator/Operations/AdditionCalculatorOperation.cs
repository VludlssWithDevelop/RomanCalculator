using RomanCalculator.Core.Operations.Common;

namespace RomanCalculator.Operations
{
    /// <summary>
    /// Операция сложения
    /// </summary>
    public sealed class AdditionCalculatorOperation : CalculatorOperation
    {
        public AdditionCalculatorOperation(int executePriority, char operationMark) 
            : base(executePriority, operationMark)
        {
        }

        public override decimal Evaluate(string input) 
            => LeftOperand + RightOperand;
    }
}
