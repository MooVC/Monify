namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to support <see cref="IEquatable{T}"/>.
/// </summary>
internal sealed class EquatableStrategy
    : IStrategy
{
    private readonly Predicate<Subject> _declaration;
    private readonly Func<Subject, string> _equality;
    private readonly Predicate<Subject> _implementation;
    private readonly string _name;
    private readonly Func<Subject, string> _type;

    /// <summary>
    /// Creates a new instance of the <see cref="EquatableStrategy"/>.
    /// </summary>
    /// <param name="declaration">
    /// The condition for which, when <see langword="true" />, will result in generation of the contract declaration.
    /// </param>
    /// <param name="equality">
    /// The means by which equality is compared.
    /// </param>
    /// <param name="implementation">
    /// The condition for which, when <see langword="true" />, will result in generation of the implementation.
    /// </param>
    /// <param name="name">
    /// The name of the equality operator, used as part of the hint name for the generated code.
    /// </param>
    /// <param name="type">
    /// The qualification for the type which serves as the subject for comparison.
    /// </param>
    public EquatableStrategy(Predicate<Subject> declaration, Func<Subject, string> equality, Predicate<Subject> implementation, string name, Func<Subject, string> type)
    {
        _declaration = declaration;
        _equality = equality;
        _implementation = implementation;
        _name = name;
        _type = type;
    }

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        string type = _type(subject);

        if (_declaration(subject))
        {
            yield return GenerateContract(subject, type);
        }

        if (_implementation(subject))
        {
            yield return GenerateImplementation(subject, type);
        }
    }

    private Source GenerateContract(Subject subject, string type)
    {
        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
                : IEquatable<{{type}}>
            {
            }
            """;

        return new Source(code, $"{nameof(IEquatable<Subject>)}.{_name}");
    }

    private Source GenerateImplementation(Subject subject, string type)
    {
        string code = $$"""
            partial class {{subject.Qualification}}
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
            
                    return {{_equality(subject)}};
                }
            }
            """;

        return new Source(code, $"{nameof(IEquatable<Subject>)}.{_name}.{nameof(Equals)}");
    }
}