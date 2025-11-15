namespace Monify.Console.Tests.Classes.Nested.Demonstrations.NestedConversionScenariosTests;

using Monify.Console.Classes.Nested.Demonstrations;
using Shouldly;
using Xunit;

/// <summary>
/// Tests for <see cref="NestedConversionScenarios.ConvertOuterToString(OuterStringValue)"/>.
/// </summary>
public sealed class WhenConvertOuterToStringIsCalled
{
    private const string Value = "Nested";

    [Fact]
    public void GivenOuterWrapperThenTheOriginalStringIsReturned()
    {
        OuterStringValue outer = NestedConversionScenarios.ConvertStringToOuter(Value);

        string result = NestedConversionScenarios.ConvertOuterToString(outer);

        result.ShouldBe(Value);
    }
}