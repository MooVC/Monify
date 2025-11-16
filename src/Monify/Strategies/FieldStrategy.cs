namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to define the field that holds the state of the encapsulated value.
/// </summary>
internal sealed class FieldStrategy
    : IStrategy
{
    /// <summary>
    /// Defines the name of the field used to store the state of the encapsulared value.
    /// </summary>
    public const string Name = "_value";

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (subject.HasField)
        {
            yield break;
        }

        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                private readonly {{subject.Value}} {{Name}};
            }
            """;

        yield return new Source(code, Name);
    }
}