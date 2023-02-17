namespace RomanCalculator.Core.Contracts
{
    public interface ICalculator
    {
        /// <summary>
        /// Посчитать математическое выражение с римскими числами
        /// </summary>
        /// <param name="input">Математическое выражение с римскими числами</param>
        /// <returns>Ответ римским числом</returns>
        string Evaluate(string input);
    }
}
