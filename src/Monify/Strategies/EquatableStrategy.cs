namespace Monify.Strategies;

using Monify.Model;
using static Monify.Model.Subject;

/// <summary>
/// Generates the source needed to support <see cref="IEquatable{T}"/>.
/// </summary>
internal sealed class EquatableStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        IEnumerable<Source> equatables = GetEquatables(
            () => $"Equals(other.{FieldStrategy.Name})",
            subject.IsEquatable,
            subject.HasEquatable,
            "Self",
            subject,
            subject.Qualification);

        for (int index = 0; index < subject.Encapsulated.Length; index++)
        {
            Encapsulated encapsulated = subject.Encapsulated[index];

            string hint = index == IndexForEncapsulatedValue
                ? "Value"
                : $"Passthrough.{index:D2}";

            IEnumerable<Source> field = GetEquatables(
                () => GetEqualityOperator(encapsulated),
                encapsulated.IsEquatable,
                encapsulated.HasEquatable,
                hint,
                subject,
                encapsulated.Type);

            equatables = equatables.Concat(field);
        }

        return equatables;
    }

    private static IEnumerable<Source> GetEquatables(
        Func<string> implementation,
        bool isEquatable,
        bool hasEquatable,
        string name,
        Subject subject,
        string type)
    {
        if (!isEquatable)
        {
            yield return GenerateContract(name, subject, type);
        }

        if (!hasEquatable)
        {
            yield return GenerateImplementation(implementation, name, subject, type);
        }
    }

    private static Source GenerateContract(string name, Subject subject, string type)
    {
        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}} : IEquatable<{{type}}>
            {
            }
            """;

        return new Source(code, $"{nameof(IEquatable<Subject>)}.{name}");
    }

    private static string GetEqualityOperator(Encapsulated encapsulated)
    {
        if (encapsulated.IsSequence)
        {
            string check = $"global::Monify.Internal.SequenceEqualityComparer.Default.Equals({FieldStrategy.Name}, other)";

            if (IsImmutableArray(encapsulated.Type))
            {
                check = $"{FieldStrategy.Name}.IsDefault ? other.IsDefault : !other.IsDefault && {check}";
            }

            return check;
        }

        return $"global::System.Collections.Generic.EqualityComparer<{encapsulated.Type}>.Default.Equals({FieldStrategy.Name}, other)";
    }

    private static Source GenerateImplementation(Func<string> implementation, string name, Subject subject, string type)
    {
        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public bool Equals({{type}} other)
                {
                    if (ReferenceEquals(this, other))
                    {
                        return true;
                    }

                    if (ReferenceEquals(other, null))
                    {
                        return false;
                    }

                    return {{implementation()}};
                }
            }
            """;

        return new Source(code, $"{nameof(IEquatable<Subject>)}.{name}.{nameof(Equals)}");
    }

    private static bool IsImmutableArray(string value)
    {
        return value.StartsWith("global::System.Collections.Immutable.ImmutableArray<", StringComparison.Ordinal);
    }
}