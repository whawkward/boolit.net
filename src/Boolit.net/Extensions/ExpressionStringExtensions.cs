namespace Boolit.NET.Extensions;
internal static class ExpressionStringExtensions
{
    /// <summary>
    /// Inserts an asterisk ('*') marker into the specified string at the given index, handling whitespace and edge cases.
    /// </summary>
    /// <param name="expression">The input string into which the marker will be inserted.</param>
    /// <param name="index">The position at which to insert the marker.</param>
    /// <returns>A new string with an asterisk inserted at the specified index.</returns>
    /// <exception cref="ArgumentException">Thrown if the input string is null or empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is outside the bounds of the string.</exception>
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