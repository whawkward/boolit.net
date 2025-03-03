using Boolify.NET.Parsing;

namespace Boolify.NET;

public sealed class BoolExpression
{
    private readonly Lazy<bool> _lazyEvaluation;

    private BoolExpression(string expression)
    {
        Expression = expression;
        _lazyEvaluation = new(ValueFactory);
    }

    public string Expression { get; }
    public bool Evaluate() => _lazyEvaluation.Value;
    public override string ToString() => $"Expression: \"{Expression}\" evaluates to {_lazyEvaluation.Value}";

    private bool ValueFactory()
    {
        var lexer = new Lexer(Expression);
        var ast = lexer.ToAst();
        return ast.Visit();
    }

    public static BoolExpression Create(string expression)
        => !string.IsNullOrWhiteSpace(expression) ?
            new(expression) :
            throw new ArgumentException("Expression cannot be null or empty", nameof(expression));
}
