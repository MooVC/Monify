namespace Monify.Visitors.GenerateInterfaceWhenInterfaceVisitedTests;

using Monify.Model;
using InterfaceCollection = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Interfaces;
using InterfaceNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Interfaces.String;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenInterfacesThenDeclarationsAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
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
        var visitor = new GenerateInterfaceWhenInterfaceVisited();

        // Act
        Source comparable = visitor.Observe(InterfaceAt(subject, encapsulatedIndex: 0, interfaceIndex: 0)).Single();
        Source genericComparable = visitor.Observe(InterfaceAt(subject, encapsulatedIndex: 0, interfaceIndex: 1)).Single();

        // Assert
        comparable.Hint.ShouldBe("Interfaces.globalSystemIComparable");
        comparable.Code.ShouldContain("SuppressMessage(\"Naming\", \"CA1710:Identifiers should have correct suffix\"");
        comparable.Code.ShouldContain("SuppressMessage(\"Design\", \"CA1036:Override methods on comparable types\"");
        comparable.Code.ShouldContain("SuppressMessage(\"Major Code Smell\", \"S1210:Comparable types should implement comparison operators\"");
        comparable.Code.ShouldContain(": global::System.IComparable");
        genericComparable.Hint.ShouldBe("Interfaces.globalSystemIComparableint");
        genericComparable.Code.ShouldContain(": global::System.IComparable<int>");
    }

    [Test]
    public void GivenNoInterfacesThenNoInterfaceSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { Interfaces = [], Type = "int" }];
        var navigator = new SubjectNavigator(new GenerationServices());

        // Act
        IEnumerable<Source> result = navigator.Navigate<Source>(subject);

        // Assert
        result.ShouldNotContain(source => source.Hint.StartsWith("Interfaces.", StringComparison.Ordinal));
    }

    [Test]
    public void GivenPassthroughInterfacesThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated { Type = "global::Sample.Inner" },
            new Encapsulated
            {
                Interfaces = ["global::System.IDisposable"],
                Type = "global::Sample.Value",
            },
        ];
        var visitor = new GenerateInterfaceWhenInterfaceVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(InterfaceAt(subject, encapsulatedIndex: 1, interfaceIndex: 0));

        // Assert
        result.ShouldBeEmpty();
    }

    private static InterfaceNode InterfaceAt(Subject subject, int encapsulatedIndex, int interfaceIndex)
    {
        var encapsulated = TestSubject.EncapsulatedAt(subject, encapsulatedIndex);
        var interfaces = new InterfaceCollection(encapsulated, subject, encapsulated._Value.Interfaces);

        return new InterfaceNode(interfaces, interfaceIndex, subject, interfaces._Value[interfaceIndex]);
    }
}