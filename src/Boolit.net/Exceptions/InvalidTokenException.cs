using Boolit.NET.Extensions;
using System.Globalization;

namespace Boolit.NET.Exceptions;

#pragma warning disable CA1032 // Implement standard exception constructors
public class InvalidTokenException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
{
    protected InvalidTokenException(string expression, int index, string messageFormat)
        : base(string.Format(CultureInfo.InvariantCulture, messageFormat, index, expression.InsertMarkerAtIndex(index)))
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class InvalidConsecutiveOperandsException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Invalid consecutive operands at index {0}; Accepted combinations are: NOT followed by OPEN_PARENTHESIS, or OPEN_PARENTHESIS, AND, OR, XOR, NOT followed by NOT; \"{1}\"";
    internal InvalidConsecutiveOperandsException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class InvalidConsecutiveBoolTokensException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Invalid consecutive tokens at index {0}; boolean values must be separated by operators: AND, OR, XOR; \"{0}\"";
    internal InvalidConsecutiveBoolTokensException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class MissingClosingParenthesisException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Expected closing parenthesis at or before index {0}; \"{1}\"";
    internal MissingClosingParenthesisException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class UnbalancedParenthesesException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unmatched closing parenthesis at index {0}; \"{1}\"";
    internal UnbalancedParenthesesException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class UnexpectedTokenException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unexpected token at index {0}; \"{1}\"";
    internal UnexpectedTokenException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class UnsupportedTokenException : InvalidTokenException
#pragma warning restore CA1032 // Implement standard exception constructors
{
    private const string _messageFormat = "Unsupported token at index {0}; \"{1}\"";
    internal UnsupportedTokenException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class UnexpectedEndOfExpressionException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
{
    internal UnexpectedEndOfExpressionException(string expression)
        : base($"Unexpected end of expression \"{expression}\"")
    {
    }
}
