using System.Text;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using RomanCalculator.Console.Enums;
using RomanCalculator.Console.Models;
using RomanCalculator.Core;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Enums;
using RomanCalculator.Core.Exceptions;
using RomanCalculator.Core.Utils;
using RomanCalculator.Extensions;

const string closeKeyWord = "close";

SetMainConsoleSettings();

var provider = ConfigureServices();
var romanCalculator = provider.GetRequiredService<ICalculator>();

var processAppArgsResult = ProcessAppArgs(args);
if (processAppArgsResult == ArgsParsingResult.ArgsNotEmptyWorkStop)
    return;

WriteHelloMessage();

while (true)
{
    WriteInputMessage();

    var userInput = Console.ReadLine();
    if (userInput.Trim() == closeKeyWord)
        return;

    ProcessUserInput(userInput);
}

void SetMainConsoleSettings()
{
    Console.Title = "Roman Calculator (BETA)";
}

ArgsParsingResult ProcessAppArgs(string[] args)
{
    if (args.Any())
    {
        var commandLineOptions = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;

        ProcessUserInput(commandLineOptions.RomanExpression);

        if (commandLineOptions.WorkContinue)
            return ArgsParsingResult.ArgsNotEmptyWorkContinue;
        else
            return ArgsParsingResult.ArgsNotEmptyWorkStop;
    }

    return ArgsParsingResult.ArgsEmptyWorkContinue;
}

void ProcessUserInput(string userInput)
{
    try
    {
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
    helloMessage.AppendLine("Additionally: you can run the app from the command line and pass the following parameters:");
    helloMessage.AppendLine("'-e' '--expr' - roman math expression without spaces");
    helloMessage.AppendLine("'-w' - continue the app work after the evaluation");
    helloMessage.AppendLine();
    helloMessage.AppendLine("Example: RomanCalculator.Console -e '(V+V)*II' -w");
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

IServiceProvider ConfigureServices()
{
    IServiceCollection services = new ServiceCollection();

    services.AddRomanCalculator(builder =>
    {
        builder.AddDefaultOperations();
    });

    return services.BuildServiceProvider();
}
