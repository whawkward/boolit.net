namespace Boolify.NET.Ast;

internal abstract class AstNode
{
    internal abstract bool Visit();

    //public bool Evaluate() => Visit();
}

internal sealed class BoolNode(bool value) : AstNode
{
    internal override bool Visit() => value;
}

internal sealed class NotNode(AstNode node) : AstNode
{
    internal override bool Visit() => !node.Visit();
}

internal sealed class AndNode(AstNode left, AstNode right) : AstNode
{
    internal override bool Visit() => left.Visit() && right.Visit();
}

internal sealed class OrNode(AstNode left, AstNode right) : AstNode
{
    internal override bool Visit() => left.Visit() || right.Visit();
}

internal sealed class XorNode(AstNode left, AstNode right) : AstNode
{
    internal override bool Visit() => left.Visit() ^ right.Visit();
}
