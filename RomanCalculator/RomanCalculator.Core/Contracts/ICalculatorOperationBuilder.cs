using System.Collections.Generic;
using RomanCalculator.Core.Operations.Common;

namespace RomanCalculator.Core.Contracts
{
    public interface ICalculatorOperationBuilder
    {
        /// <summary>
        /// Добавленные операции калькулятора
        /// </summary>
        IReadOnlyCollection<CalculatorOperation> CalculatorOperations { get; }
        /// <summary>
        /// Добавить операцию для калькулятора
        /// </summary>
        /// <param name="operation">Операция для калькулятора</param>
        /// <returns>Билдер операций</returns>
        ICalculatorOperationBuilder AddOperation(CalculatorOperation operation);
    }
}
