namespace Monify.Console.Tests.Classes.Nested.Demonstrations.NestedConversionScenariosTests;

using Monify.Console.Classes.Nested.Demonstrations;
using Shouldly;
using Xunit;

/// <summary>
/// Tests for <see cref="NestedConversionScenarios.ConvertStringToOuter(string)"/>.
/// </summary>
public sealed class WhenConvertStringToOuterIsCalled
{
    private const string Value = "Nested";

    [Fact]
    public void GivenStringValueThenTheOuterWrapperIsReturned()
    {
        OuterStringValue outer = NestedConversionScenarios.ConvertStringToOuter(Value);

        string result = outer;

        result.ShouldBe(Value);
    }
}