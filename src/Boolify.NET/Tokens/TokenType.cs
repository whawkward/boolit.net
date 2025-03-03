namespace Boolify.NET.Tokens;

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

//internal sealed class TokenSet
//{
//    private TokenSet(IDictionary<string, TokenType> tokens)
//    {
//    }
//    public ImmutableDictionary<string, TokenType> TokenValueToType { get; }
//}

// TODO: Implement a TokenSetBuilder that builds a TokenSet.
// When it's built, it will fail if no words/symbols are present for certain TokenType values.