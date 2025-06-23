namespace Boolit.NET.Tokens;

internal interface IToken;

internal readonly record struct BoolToken(bool Value) : IToken;

internal interface IOperandToken : IToken;

internal readonly record struct AndToken : IOperandToken;

internal readonly record struct OrToken : IOperandToken;

internal readonly record struct XorToken : IOperandToken;

internal readonly record struct NotToken : IOperandToken;

internal readonly record struct OpenParenthesisToken : IOperandToken;

internal readonly record struct CloseParenthesisToken : IOperandToken;
