namespace Boolit.NET.Tokens;

public enum TokenType
{
    BoolTrue,
    BoolFalse,
    And,
    Or,
    Xor,
    Not,
    OpenParenthesis,
    CloseParenthesis
}

internal static class TokenTypeExtensions
{
    /// <summary>
        /// Converts a <see cref="TokenType"/> value to its corresponding <see cref="IToken"/> instance.
        /// </summary>
        /// <param name="type">The token type to convert.</param>
        /// <returns>The token instance representing the specified token type.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the token type is not recognized.</exception>
        public static IToken ToToken(this TokenType type)
        => type switch
        {
            TokenType.BoolTrue => new BoolToken(true),
            TokenType.BoolFalse => new BoolToken(false),
            TokenType.And => new AndToken(),
            TokenType.Or => new OrToken(),
            TokenType.Xor => new XorToken(),
            TokenType.Not => new NotToken(),
            TokenType.OpenParenthesis => new OpenParenthesisToken(),
            TokenType.CloseParenthesis => new CloseParenthesisToken(),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
}