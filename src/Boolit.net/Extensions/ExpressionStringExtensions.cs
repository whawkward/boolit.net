namespace Boolit.NET.Extensions;

internal static class ExpressionStringExtensions
{
    public static string InsertMarkerAtIndex(this string expression, int index)
    {
        if (string.IsNullOrEmpty(expression))
        {
            throw new ArgumentException("Expression cannot be null or empty.", nameof(expression));
        }

        if (index < 0 || index > expression.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the expression string.");
        }

        Span<char> span = stackalloc char[expression.Length + 1];

        var expressionSpan = expression.AsSpan();

        (Range copyBefore, int insertAt, Range copyAfter) = index switch
        {
            0 => (new Range(), 0, new Range(0, ^0)),
            _ when index == expressionSpan.Length - 1 => (new Range(0, ^0), index + 1, new Range()),
            _ when char.IsWhiteSpace(expressionSpan[index - 1]) => (new Range(0, index), index, new Range(index, ^0)),
            _ when char.IsWhiteSpace(expressionSpan[index + 1]) => (new Range(0, index + 1), index + 1, new Range(index + 1, ^0)),
            _ => (new Range(0, index + 1), index + 1, new Range(index + 1, ^0))
        };

        expressionSpan[copyBefore].CopyTo(span[..insertAt]);
        span[insertAt] = '*';
        expressionSpan[copyAfter].CopyTo(span[(insertAt + 1)..]);

        return span.ToString();
    }
}