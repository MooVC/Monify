namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to ensure the type can be contructed.
/// </summary>
internal sealed class ConstructorStrategy
    : IStrategy
{
    /// <summary>
    /// Defines the method name associated with the constructor.
    /// </summary>
    public const string Name = ".ctor";

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (subject.HasConstructorForEncapsulatedValue)
        {
            yield break;
        }

        string code;

        if (subject.IsImmutableArray)
        {
            code = $$"""
                {{subject.Declaration}} {{subject.Qualification}}
                {
                    public {{subject.Name}}({{subject.Value}} value)
                    {
                        if (value.IsDefault)
                        {
                            value = {{subject.Value}}.Empty;
                        }

                        {{FieldStrategy.Name}} = value;
                    }
                }
                """;
        }
        else
        {
            code = $$"""
                {{subject.Declaration}} {{subject.Qualification}}
                {
                    public {{subject.Name}}({{subject.Value}} value)
                    {
                        {{FieldStrategy.Name}} = value;
                    }
                }
                """;
        }

        yield return new Source(code, Name);
    }
}