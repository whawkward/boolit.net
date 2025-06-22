using Boolit.NET.Exceptions;

namespace Boolit.NET.UnitTests.TUnit.Tests;

internal sealed class ParenthesisValidationTests
{
#pragma warning disable S1125 // Boolean literals should not be redundant
#pragma warning disable S1764 // Identical expressions should not be used on both sides of operators
#pragma warning disable S2437 // Unnecessary bit operations should not be performed

    // Basic Valid Cases
    [Test]
    [Arguments("true && false", (true && false))]
    [Arguments("(true && false)", (true && false))]
    [Arguments("((true) && (false))", (true && false))]
    [Arguments("(true && (false || true))", (true && (false || true)))]
    // Complex Valid Cases    
    [Arguments("(true && false) || (true)", ((true && false) || true))]
    [Arguments("(((true && false)) || ((true)))", ((true && false) || true))]
    [Arguments("!(true && (false || !true))", !(true && (false || !true)))]
    [Arguments("(true && false) xor (true || !false)", ((true && false) ^ (true || !false)))]
    // Complex Operator Combinations
    [Arguments("!(!(true) && ((false)))", !(!true && false))]
    [Arguments("((true) && !(false)) || (!(true && false))", ((true && !false) || !(true && false)))]
    [Arguments("!((true && false) || (!true && (false || true)))", !((true && false) || (!true && (false || true))))]
    public async Task ValidExpressions_ShouldEvaluateCorrectly(string inputExpression, bool expectedResult)
    {
        var expression = BoolExpression.Create(inputExpression);
        var result = expression.Evaluate();
        await Assert.That(result).IsEqualTo(expectedResult);
    }

    // Missing Closing Parenthesis
    [Test]
    [Arguments("(true && false")]
    [Arguments("(true && (false || true)")]
    [Arguments("(true && (false || true")]
    [Arguments("((((true)")]
    public async Task MissingClosingParenthesis_ShouldThrowException(string inputExpression)
    {
        var expression = BoolExpression.Create(inputExpression);

        var exception = await Assert.ThrowsAsync<MissingClosingParenthesisException>(
            async () => await Task.Run(() => expression.Evaluate()).ConfigureAwait(false))
            .ConfigureAwait(false);

        await Assert.That(exception.Message).Contains("Expected closing parenthesis");
    }

    // Extra Closing Parenthesis
    [Test]
    [Arguments("true && false)")]
    [Arguments("(true) && false))")]
    [Arguments("true))))))")]
    [Arguments("(true))) && ((false)")]
    public async Task ExtraClosingParenthesis_ShouldThrowException(string inputExpression)
    {
        var expression = BoolExpression.Create(inputExpression);

        var exception = await Assert.ThrowsAsync<UnbalancedParenthesesException>(
            async () => await Task.Run(() => expression.Evaluate()).ConfigureAwait(false))
            .ConfigureAwait(false);

        await Assert.That(exception.Message).Contains("Unmatched closing parenthesis");
    }

    // Complex Invalid Cases
    [Test]
    [Arguments("(true && false)) || ((true")]
    [Arguments(")(")]
    [Arguments(")true && false(")]
    public async Task ComplexInvalidParentheses_ShouldThrowException(string inputExpression)
    {
        var expression = BoolExpression.Create(inputExpression);

        await Assert.ThrowsAsync<InvalidTokenException>(
            async () => await Task.Run(() => expression.Evaluate()).ConfigureAwait(false))
            .ConfigureAwait(false);
    }

    // Edge Cases
    [Test]
    [Arguments("()")]
    [Arguments("(())")]
    [Arguments("true && () || false")]
    [Arguments("((true) && ()) || (false)")]
    public async Task EmptyParentheses_ShouldThrowInvalidConsecutiveOperandsException(string inputExpression)
    {
        var expression = BoolExpression.Create(inputExpression);

        await Assert.ThrowsAsync<InvalidConsecutiveOperandsException>(
            async () => await Task.Run(() => expression.Evaluate()).ConfigureAwait(false))
            .ConfigureAwait(false);
    }

    // Boundary Cases
    [Test]
    [Arguments("(", typeof(UnexpectedEndOfExpressionException))]
    [Arguments(")", typeof(UnexpectedTokenException))]
    [Arguments("true && ( || false", typeof(InvalidConsecutiveOperandsException))]
    [Arguments("true && ) || false", typeof(InvalidConsecutiveOperandsException))]
    public async Task BoundaryCases_ShouldThrowException(string inputExpression, Type exception)
    {
        var expression = BoolExpression.Create(inputExpression);

        await Assert.ThrowsAsync(exception,
            async () => await Task.Run(() => expression.Evaluate()).ConfigureAwait(false)).ConfigureAwait(false);
    }


    // Specific Mixed Error Cases
    [Test]
    [Arguments("true))))))", "Unmatched closing parenthesis")]
    [Arguments("(true && (false", "Expected closing parenthesis")]
    [Arguments("(true && ) || (false", "Invalid consecutive operands")]
    public async Task SpecificErrorCases_ShouldThrowAppropriateExceptions(string inputExpression, string expectedErrorMessage)
    {
        var expression = BoolExpression.Create(inputExpression);

        var exception = await Assert.ThrowsAsync<InvalidTokenException>(
            async () => await Task.Run(() => expression.Evaluate()).ConfigureAwait(false))
            .ConfigureAwait(false);

        await Assert.That(exception.Message).Contains(expectedErrorMessage);
    }
#pragma warning restore S2437 // Unnecessary bit operations should not be performed
#pragma warning restore S1764 // Identical expressions should not be used on both sides of operators
#pragma warning restore S1125 // Boolean literals should not be redundant
}
