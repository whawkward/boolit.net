// See https://aka.ms/new-console-template for more information
using Boolify.NET;

Console.WriteLine("Hello, World!");

TestCase[] tests = [
    new ("true and TRUE", true),
    new ("true and FALSE", false),
    new ("true or TRUE", true),
    new ("true or FALSE", true),
    new ("true xor TRUE", false),
    new ("true xor FALSE", true),
    new ("not true", false),
    new ("not false", true),
    new ("true and not true", false),
    new ("true or not true", true),
    new ("true xor not true", true),
    new ("!true", false)
    ];

foreach (var test in tests)
{
    Console.WriteLine($"Testing: {test.Expression}");
    try
    {
        var expressions = BoolExpression.Create(test.Expression);
        var result = expressions.Evaluate();
        Console.WriteLine($"Expected: {test.ExpectedResult} - Actual: {result}\n\n**\n");
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error: {ex.Message}\n\n**\n");
    }
}

internal sealed record TestCase(string Expression, bool ExpectedResult);