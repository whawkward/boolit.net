namespace Boolit.NET.Tokens;

internal static class ConsecutiveOperandsValidator
{
    public static readonly HashSet<(Type, Type)> ValidConsecutiveOperands =
    [
       (typeof(OpenParenthesisToken), typeof(NotToken)),
        (typeof(AndToken), typeof(NotToken)),
        (typeof(OrToken), typeof(NotToken)),
        (typeof(XorToken), typeof(NotToken)),
        (typeof(NotToken), typeof(NotToken)),
        (typeof(NotToken), typeof(OpenParenthesisToken)),
        (typeof(AndToken), typeof(OpenParenthesisToken)),
        (typeof(OrToken), typeof(OpenParenthesisToken)),
        (typeof(XorToken), typeof(OpenParenthesisToken)),
        (typeof(OpenParenthesisToken), typeof(OpenParenthesisToken)),
        (typeof(CloseParenthesisToken), typeof(AndToken)),
        (typeof(CloseParenthesisToken), typeof(OrToken)),
        (typeof(CloseParenthesisToken), typeof(XorToken)),
        (typeof(CloseParenthesisToken), typeof(CloseParenthesisToken))
    ];

#pragma warning disable CA1307
    private const string _tokenSuffix = "Token";

    private static readonly IEnumerable<string> _groupedNames = [.. ValidConsecutiveOperands
        .Select(t =>
            (t.Item1.Name.EndsWith(_tokenSuffix, StringComparison.OrdinalIgnoreCase) ? t.Item1.Name[..^_tokenSuffix.Length] : t.Item1.Name,
            t.Item2.Name.EndsWith(_tokenSuffix, StringComparison.OrdinalIgnoreCase) ? t.Item2.Name[..^_tokenSuffix.Length] : t.Item2.Name)
        )
        .GroupBy(t => t.Item1)
        .OrderBy(n => n.Key)
        .Select(g => $"{g.Key} followed by {string.Join("|", g.Select(x => x.Item2))}")];
#pragma warning restore CA1307

    public static readonly string ValidCombinationsMessage =
        $"Accepted combinations are: {string.Join(", or ", _groupedNames)}";

}
