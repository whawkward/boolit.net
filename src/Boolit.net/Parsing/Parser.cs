using Boolit.NET.Ast;
using Boolit.NET.Exceptions;
using Boolit.NET.Tokens;

namespace Boolit.NET.Parsing;
internal static class Parser
{
    public static IAstNode ToAst(this Lexer lexer)
    {
        int parenCount = 0;
        var node = ParseExpression(ref lexer, ref parenCount);

        return node;
    }

    private static IAstNode ParseExpression(ref Lexer lexer, ref int parenCount)
    {
        var node = ParseTerm(ref lexer, ref parenCount);

        while (lexer.Advance() && lexer.Current is AndToken or OrToken or XorToken)
        {
            switch (lexer.Current)
            {
                case AndToken:
                    node = new AndNode(node, ParseTerm(ref lexer, ref parenCount));
                    break;
                case OrToken:
                    node = new OrNode(node, ParseTerm(ref lexer, ref parenCount));
                    break;
                case XorToken:
                    node = new XorNode(node, ParseTerm(ref lexer, ref parenCount));
                    break;
            }
        }

        // We've exited the loop, which means we've either:
        // 1. Run out of tokens (Advance returned false)
        // 2. Found a token that isn't an operator (possibly a closing parenthesis)

        // If we encounter a closing parenthesis without an opening one
        if (lexer.Current is CloseParenthesisToken && parenCount == 0)
        {
            throw new UnbalancedParenthesesException(lexer.InputExpression, lexer.CurrentIndex);
        }

        return node;
    }

    private static IAstNode ParseTerm(ref Lexer lexer, ref int parenCount)
    {
        if (!lexer.Advance())
        {
            throw new UnexpectedEndOfExpressionException(lexer.InputExpression);
        }

        var token = lexer.Current;

        switch (token)
        {
            case BoolToken boolToken:
                return new BoolNode(boolToken.Value);

            case NotToken:
                return new NotNode(ParseTerm(ref lexer, ref parenCount));

            case OpenParenthesisToken:
                parenCount++; // Track opening parenthesis
                var node = ParseExpression(ref lexer, ref parenCount);

                if (lexer.Current is not CloseParenthesisToken)
                {
                    throw new MissingClosingParenthesisException(lexer.InputExpression,
                        lexer.CurrentIndex == -1 ? lexer.InputExpression.Length - 1 : lexer.CurrentIndex);
                }

                // We found the matching closing parenthesis
                parenCount--;
                return node;
            default:
                throw new UnexpectedTokenException(lexer.InputExpression, lexer.CurrentIndex);
        }
    }
}
