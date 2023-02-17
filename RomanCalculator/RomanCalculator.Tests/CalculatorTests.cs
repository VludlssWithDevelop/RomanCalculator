using FluentAssertions;
using RomanCalculator.Core.Contracts;
using RomanCalculator.Core.Enums;
using RomanCalculator.Core.Exceptions;
using RomanCalculator.Core.Utils;

namespace RomanCalculator.Tests
{
    public class CalculatorTests
    {
        private readonly ICalculator _calculator;

        public CalculatorTests()
        {
            _calculator = Calculator.CreateDefault();
        }

        [Theory]
        [InlineData("X + XI", "XXI")]
        [InlineData("X + -V", "V")]
        [InlineData("-V + X", "V")]
        [InlineData("-   V    +  X", "V")]
        [InlineData("X.III + X.II", "XX,D")]
        public void AdditionOperation_ShouldBe_EvaluateSuccess(string userInput, string expectedResult)
        {
            var evaluateResult = _calculator.Evaluate(userInput);

            evaluateResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("X / II", "V")]
        [InlineData("-X / -II", "V")]
        [InlineData("-   X    /   -  II", "V")]
        public void DivisionOperation_ShouldBe_EvaluateSuccess(string userInput, string expectedResult)
        {
            var evaluateResult = _calculator.Evaluate(userInput);

            evaluateResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("III * III", "IX")]
        [InlineData("-II * -IV", "VIII")]
        [InlineData("-   II  *  -      IV", "VIII")]
        public void MultiplicationOperation_ShouldBe_EvaluateSuccess(string userInput, string expectedResult)
        {
            var evaluateResult = _calculator.Evaluate(userInput);

            evaluateResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("X - V", "V")]
        [InlineData("-V - -VI", "I")]
        [InlineData("-   V  - -      VI", "I")]
        public void SubstractionOperation_ShouldBe_EvaluateSuccess(string userInput, string expectedResult)
        {
            var evaluateResult = _calculator.Evaluate(userInput);

            evaluateResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("III + III * II", "IX")]
        [InlineData("(III + III) * II", "XII")]
        [InlineData("(III + III) * (II * II)", "XXIV")]
        public void Priority_ShouldBe_Respected(string userInput, string expectedResult)
        {
            var evaluateResult = _calculator.Evaluate(userInput);

            evaluateResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("X", ErrorCodes.InvalidMathExpression)]
        [InlineData("-X", ErrorCodes.InvalidMathExpression)]
        [InlineData("X-", ErrorCodes.InvalidOperationOrRomanNumeral)]
        [InlineData("X + X)", ErrorCodes.InvalidOperationOrRomanNumeral)]
        [InlineData("HH + HH", ErrorCodes.InvalidOperationOrRomanNumeral)]
        [InlineData("MMM + MMM", ErrorCodes.RomanNumeralOutOfRange)]
        [InlineData("X ** X", ErrorCodes.InvalidOperationOrRomanNumeral)]
        public void InvalidInput_ShouldBe_Fail(string userInput, ErrorCodes errorCode)
        {
            var errorMessage = AttributeUtils.GetEnumDescription(errorCode);
            
            Action act = () => _calculator.Evaluate(userInput);

            switch (errorCode)
            {
                case ErrorCodes.RomanNumeralOutOfRange:
                    act.Should().Throw<ArgumentOutOfRangeException>();
                    break;
                case ErrorCodes.InvalidMathExpression:
                    act.Should().Throw<InvalidMathExpressionException>();
                    break;
                case ErrorCodes.InvalidOperationOrRomanNumeral:
                    act.Should().Throw<InvalidCalculatorOperationMarkException>();
                    break;
                case ErrorCodes.InvalidRomanNumeral:
                    act.Should().Throw<InvalidEnteredRomanNumberException>();
                    break;
            }
        }
    }
}