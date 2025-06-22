using Boolit.NET.Extensions;
using System.Globalization;

namespace Boolit.NET.Exceptions;

#pragma warning disable CA1032 // Implement standard exception constructors
public class InvalidTokenException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidTokenException"/> class with a formatted error message indicating the position of an invalid token in a boolean expression.
    /// </summary>
    /// <param name="expression">The boolean expression containing the invalid token.</param>
    /// <param name="index">The zero-based index of the invalid token within the expression.</param>
    /// <param name="messageFormat">A composite format string for the error message, which will include the index and the annotated expression.</param>
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
    /// <summary>
    /// Exception thrown when two operands appear consecutively in a boolean expression without a valid operator between them.
    /// </summary>
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
    /// <summary>
    /// Exception thrown when two boolean tokens appear consecutively in an expression without an operator separating them.
    /// </summary>
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
    /// <summary>
    /// Represents an error where a closing parenthesis is missing in a boolean expression at the specified index.
    /// </summary>
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
    /// <summary>
    /// Exception thrown when an unmatched closing parenthesis is encountered in a boolean expression.
    /// </summary>
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
    /// <summary>
    /// Exception thrown when an unexpected token is encountered at the specified index in a boolean expression.
    /// </summary>
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
    /// <summary>
    /// Exception thrown when an unsupported token is encountered in a boolean expression at the specified index.
    /// </summary>
    internal UnsupportedTokenException(string expression, int index)
        : base(expression, index, _messageFormat)
    {
    }
}

#pragma warning disable CA1032 // Implement standard exception constructors
public class UnexpectedEndOfExpressionException : Exception
#pragma warning restore CA1032 // Implement standard exception constructors
{
    /// <summary>
    /// Exception thrown when a boolean expression ends unexpectedly before completion.
    /// </summary>
    internal UnexpectedEndOfExpressionException(string expression)
        : base($"Unexpected end of expression \"{expression}\"")
    {
    }
}
