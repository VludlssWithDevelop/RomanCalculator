using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RomanCalculator.Core;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Enums;
using RomanCalculator.Core.Exceptions;
using RomanCalculator.Core.Utils;
using RomanCalculator.Extensions;

const string closeKeyWord = "close";

IServiceCollection services = new ServiceCollection();
services.AddRomanCalculator();

var provider = services.BuildServiceProvider();

var romanCalculator = provider.GetRequiredService<ICalculator>();

WriteHelloMessage();

while (true)
{
    try
    {
        WriteInputMessage();

        var userInput = Console.ReadLine();
        if (userInput.Trim() == closeKeyWord)
            return;

        var romanCalculatorEvaluateResult = romanCalculator.Evaluate(userInput);
        WriteCalculatorEvaluateResult(romanCalculatorEvaluateResult);
    }
    catch (ArgumentOutOfRangeException)
    {
        WriteError(AttributeUtils.GetEnumDescription(ErrorCodes.RomanNumeralOutOfRange));
    }
    catch (FormatException)
    {
        WriteError(AttributeUtils.GetEnumDescription(ErrorCodes.RomanNumeralOutOfRange));
    }
    catch (InvalidMathExpressionException)
    {
        WriteError(AttributeUtils.GetEnumDescription(ErrorCodes.InvalidMathExpression));
    }
    catch (InvalidCalculatorOperationMarkException)
    {
        WriteError(AttributeUtils.GetEnumDescription(ErrorCodes.InvalidOperationOrRomanNumeral));
    }
    catch (InvalidEnteredRomanNumberException ex)
    {
        var errorMessage = AttributeUtils.GetEnumDescription(ErrorCodes.InvalidRomanNumeral);
        var formattedErrorMessage = string.Format(errorMessage, ex.InvalidRomanNumber);
        
        WriteError(formattedErrorMessage);
    }
}

void WriteHelloMessage()
{
    var helloMessage = new StringBuilder();
    helloMessage.AppendLine("Roman number calculator");
    helloMessage.AppendLine();
    helloMessage.AppendLine($"Available roman characters: {CalculatorConstants.ValidRomanNumberChars}");
    helloMessage.AppendLine("Available math operations: + - * / ()");
    helloMessage.AppendLine("Available numbers: positive, negative, float number");
    helloMessage.AppendLine();
    helloMessage.AppendLine($"Type '{closeKeyWord}' if you want to stop work with this app.");
    helloMessage.AppendLine();
    helloMessage.AppendLine("Supported feature for developers: adding custom math operations");
    helloMessage.AppendLine();

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(helloMessage.ToString());
    Console.ResetColor();
}

void WriteInputMessage()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("Input roman expression: ");
    Console.ResetColor();
}

void WriteCalculatorEvaluateResult(string calculatorEvaluateResult)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"Calculator evaluate result: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(calculatorEvaluateResult);
    Console.WriteLine();
    Console.ResetColor();
}

void WriteError(string text)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.WriteLine();
    Console.ResetColor();
}
