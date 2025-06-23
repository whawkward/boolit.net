namespace Boolit.NET.Ast;

internal interface IAstNode
{
    bool Visit();
}

internal sealed class BoolNode(bool value) : IAstNode
{
    public bool Visit() => value;
}

internal sealed class NotNode(IAstNode node) : IAstNode
{
    public bool Visit() => !node.Visit();
}

internal sealed class AndNode(IAstNode left, IAstNode right) : IAstNode
{
    public bool Visit() => left.Visit() && right.Visit();
}

internal sealed class OrNode(IAstNode left, IAstNode right) : IAstNode
{
    public bool Visit() => left.Visit() || right.Visit();
}

internal sealed class XorNode(IAstNode left, IAstNode right) : IAstNode
{
    public bool Visit() => left.Visit() ^ right.Visit();
}
