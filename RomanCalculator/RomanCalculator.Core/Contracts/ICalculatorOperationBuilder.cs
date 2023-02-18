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
        ICalculatorOperationBuilder AddOperation(CalculatorOperation operation);
        /// <summary>
        /// Добавить операции для калькулятора "из коробки"
        /// </summary>
        ICalculatorOperationBuilder AddDefaultOperations();
        /// <summary>
        /// Добавить операцию сложения
        /// </summary>
        ICalculatorOperationBuilder AddAdditionOperation(int executePriority = 2, char operationMark = '+');
        /// <summary>
        /// Добавить операцию деления
        /// </summary>
        ICalculatorOperationBuilder AddDivisionOperation(int executePriority = 1, char operationMark = '/');
        /// <summary>
        /// Добавить операцию умножения
        /// </summary>
        ICalculatorOperationBuilder AddMultiplicationOperation(int executePriority = 1, char operationMark = '*');
        /// <summary>
        /// Добавить операцию вычитания
        /// </summary>
        ICalculatorOperationBuilder AddSubstractionOperation(int executePriority = 2, char operationMark = '-');
    }
}
