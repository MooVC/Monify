namespace Monify.Strategies;

using Monify.Model;
using static Monify.Model.Subject;

/// <summary>
/// Generates interface declarations to forward interfaces supported by the encapsulated type.
/// </summary>
internal sealed class InterfaceDeclarationStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (subject.Encapsulated.IsDefaultOrEmpty)
        {
            yield break;
        }

        Encapsulated encapsulated = subject.Encapsulated[IndexForEncapsulatedValue];

        if (encapsulated.Interfaces.IsDefaultOrEmpty)
        {
            yield break;
        }

        foreach (string @interface in encapsulated.Interfaces)
        {
            string hint = $"Interfaces.{@interface.NormalizeTypeForHint()}";

            string code = $$"""
                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                {{subject.Declaration}} {{subject.Qualification}}
                    : {{@interface}}
                {
                }
                """;

            yield return new Source(code, hint);
        }
    }
}