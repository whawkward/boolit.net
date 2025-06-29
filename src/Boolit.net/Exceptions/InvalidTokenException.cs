using Boolit.NET.Extensions;
using Boolit.NET.Tokens;
using System.Globalization;

namespace Boolit.NET.Exceptions;

#pragma warning disable CA1032 // Implement standard exception constructors
public abstract class InvalidTokenException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
{
    protected InvalidTokenException(string expression, int index, string messageFormat)
        : base(string.Format(CultureInfo.InvariantCulture, messageFormat, index, expression.InsertMarkerAtIndex(index)))
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class InvalidConsecutiveOperandsException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private static readonly string _messageFormat = $"Invalid consecutive operands at index {{0}}; Accepted combinations are: {ConsecutiveOperandsValidator.ValidCombinationsMessage}; \"{{1}}\"";
    internal InvalidConsecutiveOperandsException(string expression, int index)
#pragma warning disable CA1307
        : base(expression, index, _messageFormat)
#pragma warning restore CA1307
 
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class InvalidConsecutiveBoolTokensException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Invalid consecutive tokens at index {0}; boolean values must be separated by operators: AND, OR, XOR; \"{1}\"";
    internal InvalidConsecutiveBoolTokensException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class MissingClosingParenthesisException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Expected closing parenthesis at or before index {0}; \"{1}\"";
    internal MissingClosingParenthesisException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class UnbalancedParenthesesException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unmatched closing parenthesis at index {0}; \"{1}\"";
    internal UnbalancedParenthesesException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class UnexpectedTokenException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unexpected token at index {0}; \"{1}\"";
    internal UnexpectedTokenException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class UnsupportedTokenException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unsupported token at index {0}; \"{1}\"";
    internal UnsupportedTokenException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public sealed class UnexpectedEndOfExpressionException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unexpected end of expression \"{0}\"";
    internal UnexpectedEndOfExpressionException(string expression)
        : base(expression, expression.Length - 1, _messageFormat)
    {
    }
}
