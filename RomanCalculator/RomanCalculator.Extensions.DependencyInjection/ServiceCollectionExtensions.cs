using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RomanCalculator.Core.Contracts;

namespace RomanCalculator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Зарегистрировать калькулятор, с базовыми настройками, в DI контейнере
        /// </summary>
        public static IServiceCollection AddRomanCalculatorDefault(this IServiceCollection services)
        {
            services.TryAddSingleton<ICalculator>(provider => Calculator.CreateDefault());

            return services;
        }

        /// <summary>
        /// Зарегистрировать калькулятор в DI контейнере
        /// </summary>
        /// <param name="calculatorBuilderFunc">Callback для добавления кастомных операций</param>
        public static IServiceCollection AddRomanCalculator(this IServiceCollection services, Action<ICalculatorOperationBuilder> calculatorBuilderFunc)
        {
            services.TryAddSingleton<ICalculator>(provider => Calculator.Create(calculatorBuilderFunc));

            return services;
        }
    }
}
