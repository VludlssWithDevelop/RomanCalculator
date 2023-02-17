using System.ComponentModel;

namespace RomanCalculator.Core.Enums
{
    public enum ErrorCodes
    {
        [Description("Roman numeral can't be smaller than 1 or larger than 3999")]
        RomanNumeralOutOfRange = 1000,
        [Description("Invalid math expression entered!")]
        InvalidMathExpression = 1001,
        [Description("Invalid operation(s) or roman number(s) entered!")]
        InvalidOperationOrRomanNumeral = 1002,
        [Description("Invalid roman number(s) entered! Invalid roman number: {0}")]
        InvalidRomanNumeral = 1003,
    }
}
