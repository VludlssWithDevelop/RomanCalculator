using CommandLine;

namespace RomanCalculator.Console.Models
{
    public class CommandLineOptions
    {
        [Option('e', "expr", Required = true, HelpText = "Input roman expression.", Separator = ' ')]
        public string RomanExpression { get; set; }
        [Option('w', Default = false, Required = false, HelpText = "Input '-w', if you want continue to work with the app.")]
        public bool WorkContinue { get; set; }
    }
}
