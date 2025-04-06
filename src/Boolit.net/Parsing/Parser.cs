using Boolit.net.Ast;
using Boolit.net.Tokens;

namespace Boolit.net.Parsing;
internal static class Parser
{
    public static AstNode ToAst(this Lexer lexer)
        => ParseExpression(ref lexer);

    private static AstNode ParseExpression(ref Lexer lexer)
    {
        var node = ParseTerm(ref lexer);

        while (lexer.Advance() && (lexer.Current is AndToken or OrToken or XorToken))
        {
            switch (lexer.Current)
            {
                case AndToken:
                    node = new AndNode(node, ParseTerm(ref lexer));
                    break;
                case OrToken:
                    node = new OrNode(node, ParseTerm(ref lexer));
                    break;
                case XorToken:
                    node = new XorNode(node, ParseTerm(ref lexer));
                    break;
            }
        }
        return node;
    }

    private static AstNode ParseTerm(ref Lexer lexer)
    {
        if (!lexer.Advance())
        {
            throw new Exception(/*TODO: Replace with custom exception*/"Unexpected end of expression");
        }

        var token = lexer.Current;

        switch (token)
        {
            case BoolToken boolToken:
                return new BoolNode(boolToken.Value);
            case NotToken:
                return new NotNode(ParseTerm(ref lexer));
            case OpenParenthesisToken:
                var node = ParseExpression(ref lexer);
                if (lexer.Current is not CloseParenthesisToken)
                {
                    throw new Exception(/*TODO: Replace with custom exception*/$"Expected closing parenthesis at index {lexer.CurrentIndex}");
                }

                return node;

            default:
                throw new Exception(/*TODO: Replace with custom exception*/$"Unexpected token at index {lexer.CurrentIndex}");
        }

    }
}
