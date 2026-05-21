namespace Monify.Snippets;

using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Arrangements = Monify.Snippets.Extensions;

[DebuggerDisplay("{Name,nq}")]
public sealed record Snippets(
    Content[] Body,
    Content Declaration,
    Generated[] Expected,
    Extension[] Extensions,
    string Name,
    LanguageVersion Minimum = LanguageVersion.CSharp1,
    LanguageVersion Maximum = LanguageVersion.Latest)
{
    public const string BodyTag = "__BODY__";

    public IEnumerable<Expectations> Render(Arrangements target)
    {
        IEnumerable<Content> declarations = Compose();

        Arrangements[] arrangements = Enum.GetValues<Arrangements>();

        foreach (Arrangements arrangement in arrangements)
        {
            if (arrangement == Arrangements.None || target.HasFlag(arrangement))
            {
                foreach (Expectations expectation in Expect(arrangement, declarations))
                {
                    yield return expectation;
                }
            }
        }
    }

    private IEnumerable<Content> Compose()
    {
        if (Body.Length == 0)
        {
            foreach (Content content in Compose(Declaration))
            {
                yield return content;
            }
        }
        else
        {
            foreach (Content body in Body)
            {
                string declaration = Declaration.Body.Replace(BodyTag, body.Body);
                Content content = new(declaration, body.Minimum, body.Maximum);

                foreach (Content composition in Compose(content))
                {
                    yield return composition;
                }
            }
        }
    }

    private IEnumerable<Content> Compose(Content body)
    {
        LanguageVersion minimum = Highest(Highest(body.Minimum, Declaration.Minimum), Minimum);
        LanguageVersion maximum = Lowest(Lowest(body.Maximum, Declaration.Maximum), Maximum);

        if (minimum <= maximum)
        {
            yield return new(body.Body, minimum, maximum);
        }
    }

    private IEnumerable<Expectations> Expect(Arrangements arrangements, IEnumerable<Content> declarations)
    {
        Generated[] expectations = Expected
            .Where(extension => extension.Extensions == Arrangements.None || !arrangements.HasFlag(extension.Extensions))
            .ToArray();

        string[] extensions = Extensions
            .Where(extension => arrangements.HasFlag(extension.Extensions))
            .Select(extension => extension.Body)
            .ToArray();

        foreach (Content declaration in declarations)
        {
            string[] contents = [declaration.Body, .. extensions];

            yield return new Expectations(contents, expectations, declaration.Minimum, declaration.Maximum);
        }
    }

    private static LanguageVersion Highest(LanguageVersion first, LanguageVersion second)
    {
        return first > second
            ? first
            : second;
    }

    private static LanguageVersion Lowest(LanguageVersion first, LanguageVersion second)
    {
        return first < second
            ? first
            : second;
    }
}