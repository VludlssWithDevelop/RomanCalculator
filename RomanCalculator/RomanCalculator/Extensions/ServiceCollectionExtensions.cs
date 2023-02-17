using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Operations;

namespace RomanCalculator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Зарегистрировать калькулятор в DI контейнере
        /// </summary>
        public static IServiceCollection AddRomanCalculator(this IServiceCollection services)
        {
            var calculatorBuilder = CreateDefaultRomanCalculatorBuilder();

            services.TryAddSingleton<ICalculator>(provider => new Calculator(calculatorBuilder));

            return services;
        }

        /// <summary>
        /// Зарегистрировать калькулятор в DI контейнере
        /// </summary>
        /// <param name="calculatorBuilderFunc">Callback для добавления кастомных операций</param>
        public static IServiceCollection AddRomanCalculator(this IServiceCollection services, Action<ICalculatorOperationBuilder> calculatorBuilderFunc)
        {
            var calculatorBuilder = CreateDefaultRomanCalculatorBuilder();

            calculatorBuilderFunc(calculatorBuilder);

            services.TryAddSingleton<ICalculator>(provider => new Calculator(calculatorBuilder));

            return services;
        }

        private static ICalculatorOperationBuilder CreateDefaultRomanCalculatorBuilder()
        {
            var calculatorBuilder = new CalculatorBuilder()
                .AddOperation(new MultiplicationCalculatorOperation(executePriority: 1, operationMark: '*'))
                .AddOperation(new DivisionCalculatorOperation(executePriority: 1, operationMark: '/'))
                .AddOperation(new AdditionCalculatorOperation(executePriority: 2, operationMark: '+'))
                .AddOperation(new SubstractionCalculatorOperation(executePriority: 2, operationMark: '-'));

            return calculatorBuilder;
        }
    }
}
