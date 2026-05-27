namespace Monify.Strategies.InterfaceDeclarationStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenInterfacesThenDeclarationsAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
        [
            new Encapsulated
            {
                Interfaces =
                [
                    "global::System.IComparable",
                    "global::System.IComparable<int>",
                ],
                Type = "int",
            },
        ];
        var strategy = new InterfaceDeclarationStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("Interfaces.global__System_IComparable");
        sources[0].Code.ShouldContain("SuppressMessage(\"Naming\", \"CA1710:Identifiers should have correct suffix\"");
        sources[0].Code.ShouldContain("SuppressMessage(\"Design\", \"CA1036:Override methods on comparable types\"");
        sources[0].Code.ShouldContain("SuppressMessage(\"Major Code Smell\", \"S1210:Comparable types should implement comparison operators\"");
        sources[0].Code.ShouldContain(": global::System.IComparable");
        sources[1].Hint.ShouldBe("Interfaces.global__System_IComparable_int_");
        sources[1].Code.ShouldContain(": global::System.IComparable<int>");
    }

    [Fact]
    public void GivenNoInterfacesThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated = [new Encapsulated { Interfaces = [], Type = "int" }];
        var strategy = new InterfaceDeclarationStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenPassthroughInterfacesThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
        [
            new Encapsulated { Type = "global::Sample.Inner" },
            new Encapsulated
            {
                Interfaces = ["global::System.IDisposable"],
                Type = "global::Sample.Value",
            },
        ];
        var strategy = new InterfaceDeclarationStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }
}