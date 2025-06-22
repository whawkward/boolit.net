namespace Boolit.NET.Ast;

internal interface IAstNode
{
    /// <summary>
/// Evaluates the boolean expression represented by this AST node and returns the result.
/// </summary>
/// <returns>The result of evaluating the boolean expression for this node.</returns>
bool Visit();
}

internal sealed class BoolNode(bool value) : IAstNode
{
    /// <summary>
/// Returns the boolean value represented by this node.
/// </summary>
/// <returns>The stored boolean value.</returns>
public bool Visit() => value;
}

internal sealed class NotNode(IAstNode node) : IAstNode
{
    /// <summary>
/// Evaluates the child node and returns the logical negation of its result.
/// </summary>
/// <returns>The boolean value representing the negated result of the child node's evaluation.</returns>
public bool Visit() => !node.Visit();
}

internal sealed class AndNode(IAstNode left, IAstNode right) : IAstNode
{
    /// <summary>
/// Evaluates the logical AND of the left and right child nodes in the boolean expression tree.
/// </summary>
/// <returns>True if both child nodes evaluate to true; otherwise, false.</returns>
public bool Visit() => left.Visit() && right.Visit();
}

internal sealed class OrNode(IAstNode left, IAstNode right) : IAstNode
{
    /// <summary>
/// Evaluates the logical OR of the left and right child nodes in the boolean expression tree.
/// </summary>
/// <returns>True if either the left or right child node evaluates to true; otherwise, false.</returns>
public bool Visit() => left.Visit() || right.Visit();
}

internal sealed class XorNode(IAstNode left, IAstNode right) : IAstNode
{
    /// <summary>
/// Evaluates the logical exclusive OR (XOR) of the left and right child nodes in the boolean expression tree.
/// </summary>
/// <returns>The result of the XOR operation between the left and right child nodes.</returns>
public bool Visit() => left.Visit() ^ right.Visit();
}
