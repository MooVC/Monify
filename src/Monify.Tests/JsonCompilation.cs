namespace Monify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

internal static class JsonCompilation
{
    private static readonly CSharpParseOptions _options = new(LanguageVersion.CSharp11);

    private static readonly MetadataReference[] _references = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!)
        .Split(Path.PathSeparator)
        .Select(path => MetadataReference.CreateFromFile(path))
        .ToArray();

    public static CSharpCompilation Create(params string[] declarations)
    {
        return CSharpCompilation.Create(
            "MonifyJsonSerialization",
            declarations.Select(declaration => CSharpSyntaxTree.ParseText(declaration, _options)),
            _references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }
}