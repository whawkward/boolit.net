using Boolit.NET.Extensions;

namespace Boolit.NET.UnitTests.TUnit.Tests;
internal sealed class InsertMarkerTests
{
    [Test]
    [Arguments("true and && true", 9, "true and *&& true")]
    [Arguments("(true or false) && true", 22,"(true or false) && true*")]
    [Arguments("true or (false xor true && false", 22, "true or (false xor true* && false")]
    public async Task MarkerIsInsertedAtCorrectIndex(string inputExpression, int index, string expectedExpression)
    {
        var result = inputExpression.InsertMarkerAtIndex(index);

        await Assert.That(result).IsEqualTo(expectedExpression);
    }
}
