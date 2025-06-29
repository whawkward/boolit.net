namespace Boolit.NET.Tokens;

internal static class ConsecutiveOperandsValidator
{
    internal static readonly HashSet<(Type, Type)> ValidConsecutiveOperands =
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
    private static readonly Dictionary<string, IEnumerable<string>> _groupedNames = ValidConsecutiveOperands
        .GroupBy(t => t.Item1.Name.Replace("Token", string.Empty))
        .OrderBy(n => n.Key)
        .ToDictionary(
            g => g.Key, 
            g => g
                .Select(x => x.Item2.Name.Replace("Token", string.Empty))
                .OrderBy(n => n)
                .AsEnumerable());
#pragma warning restore CA1307

    internal static readonly string ValidCombinationsMessage =
        $"Accepted combinations are: {string.Join(", or ", _groupedNames.Select(kvp => $"{kvp.Key} followed by {string.Join(", ",kvp.Value)}"))}";

}
