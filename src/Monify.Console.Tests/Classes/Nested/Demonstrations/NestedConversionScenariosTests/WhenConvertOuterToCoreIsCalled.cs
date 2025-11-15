namespace Monify.Console.Tests.Classes.Nested.Demonstrations.NestedConversionScenariosTests;

using Monify.Console.Classes.Nested.Demonstrations;
using Shouldly;
using Xunit;

/// <summary>
/// Tests for <see cref="NestedConversionScenarios.ConvertOuterToCore(OuterStringValue)"/>.
/// </summary>
public sealed class WhenConvertOuterToCoreIsCalled
{
    private const string Value = "Nested";

    [Fact]
    public void GivenOuterWrapperThenTheInnermostWrapperIsReturned()
    {
        OuterStringValue outer = NestedConversionScenarios.ConvertStringToOuter(Value);

        CoreStringValue core = NestedConversionScenarios.ConvertOuterToCore(outer);

        string result = core;

        result.ShouldBe(Value);
    }
}