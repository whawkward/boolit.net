using Boolit.NET.Exceptions;

namespace Boolit.NET.UnitTests.TUnit.Tests;

internal sealed class BoolExpressionTests
{
    [Test]
#pragma warning disable S1125 // Boolean literals should not be redundant
#pragma warning disable S1764 // Identical expressions should not be used on both sides of operators
#pragma warning disable S2437 // Unnecessary bit operations should not be performed
    [Arguments("true and TRUE", (true && true))]
    [Arguments("true and FALSE", (true && false))]
    [Arguments("true or TRUE", (true || true))]
    [Arguments("true or FALSE", (true || false))]
    [Arguments("true xor TRUE", (true ^ true))]
    [Arguments("true xor FALSE", (true ^ false))]
    [Arguments("not true", !true)]
    [Arguments("not false", !false)]
    [Arguments("true and not true", (true && !true))]
    [Arguments("true or not true", (true || !true))]
    [Arguments("true xor not true", (true ^ !true))]
    [Arguments("!true", !true)]
    [Arguments("!(true and true) || (true ^ false) ", !(true && true) || (true ^ false))]
    [Arguments("((false && true || true ^ true) || true ^ false)", ((false && true || true ^ true) || true) ^ false)]
#pragma warning restore S2437 // Unnecessary bit operations should not be performed
#pragma warning restore S1764 // Identical expressions should not be used on both sides of operators
#pragma warning restore S1125 // Boolean literals should not be redundant
    public async Task ValidExpression_ShouldEvaluateCorrectly(string inputExpression, bool expectedResult)
    {
        var expression = BoolExpression.Create(inputExpression);
        var result = expression.Evaluate();
        await Assert.That(result).IsEqualTo(expectedResult);
    }

    [Test]
    [Arguments("true && and false")]
    public async Task InvalidConsecutiveOperands_ShouldThrowInvalidConsecutiveOperandsException(string inputExpression)
    {
        var expression = BoolExpression.Create(inputExpression);
        

        var exception = await Assert.ThrowsAsync<InvalidConsecutiveOperandsException>(
            () => Task.FromResult(expression.Evaluate()))
            .ConfigureAwait(false);

        await Assert.That(exception.Message)
            .Contains("Invalid consecutive operands at index 8;");
    }
}
