using Boolit.NET;
using SampleConsole;

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
#pragma warning disable CA1031 // Do not catch general exception types
    try
    {
        var expressions = BoolExpression.Create(test.Expression);
        var result = expressions.Evaluate();
        Console.WriteLine($"Expected: {test.ExpectedResult} - Actual: {result}\n\n**\n");
    }
    catch (Exception ex)
    {
#pragma warning disable S6966 // Awaitable method should be used
        Console.Error.WriteLine($"Error: {ex.Message}\n\n**\n");
#pragma warning restore S6966 // Awaitable method should be used
    }
#pragma warning restore CA1031 // Do not catch general exception types
}
