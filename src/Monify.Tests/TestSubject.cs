namespace Monify;

using Monify.Model;

internal static class TestSubject
{
    public static Subject Create()
    {
        return new()
        {
            EncapsulatedValues = [new Encapsulated { Type = "int" }],
            Declaration = "class",
            Name = "Sample",
            Namespace = string.Empty,
            Nesting = [],
            Qualification = "Sample",
        };
    }

    public static Subject.Graph.EncapsulatedValues.Encapsulated EncapsulatedAt(Subject subject, int index = 0)
    {
        var values = new Subject.Graph.EncapsulatedValues(subject, subject.EncapsulatedValues);

        return new Subject.Graph.EncapsulatedValues.Encapsulated(values, index, subject, subject.EncapsulatedValues[index]);
    }
}