namespace Monify;

using System.Collections.Immutable;
using Monify.Model;

internal static class TestSubject
{
    public static Subject Create()
    {
        return new()
        {
            Conversions = ImmutableArray.Create(new Conversion
            {
                Type = "int",
            }),
            Declaration = "class",
            Name = "Sample",
            Namespace = string.Empty,
            Nesting = [],
            Qualification = "Sample",
            Value = "int",
        };
    }
}