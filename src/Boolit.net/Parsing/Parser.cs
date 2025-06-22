using Boolit.NET.Ast;
using Boolit.NET.Exceptions;
using Boolit.NET.Tokens;

namespace Boolit.NET.Parsing;
internal static class Parser
{
    /// <summary>
    /// Parses the token stream from the lexer and returns the root node of the abstract syntax tree representing the boolean expression.
    /// </summary>
    /// <returns>The root <see cref="IAstNode"/> of the parsed boolean expression AST.</returns>
    public static IAstNode ToAst(this Lexer lexer)
    {
        int parenCount = 0;
        var node = ParseExpression(ref lexer, ref parenCount);

        return node;
    }

    /// <summary>
    /// Parses a boolean expression from the lexer, handling binary operators and validating balanced parentheses.
    /// </summary>
    /// <param name="parenCount">
    /// The current count of open parentheses, passed by reference to track nesting and ensure proper matching.
    /// </param>
    /// <returns>
    /// The root <see cref="IAstNode"/> representing the parsed boolean expression.
    /// </returns>
    /// <exception cref="UnbalancedParenthesesException">
    /// Thrown if a closing parenthesis is encountered without a corresponding opening parenthesis.
    /// </exception>
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

    /// <summary>
    /// Parses a single term from the lexer, handling boolean literals, negation, and parenthesized sub-expressions.
    /// </summary>
    /// <param name="parenCount">
    /// Reference to the current count of open parentheses, used to ensure balanced grouping.
    /// </param>
    /// <returns>
    /// An <see cref="IAstNode"/> representing the parsed term.
    /// </returns>
    /// <exception cref="UnexpectedEndOfExpressionException">
    /// Thrown if the end of the input is reached unexpectedly.
    /// </exception>
    /// <exception cref="MissingClosingParenthesisException">
    /// Thrown if an opening parenthesis is not matched by a closing parenthesis.
    /// </exception>
    /// <exception cref="UnexpectedTokenException">
    /// Thrown if an unexpected token is encountered while parsing a term.
    /// </exception>
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
