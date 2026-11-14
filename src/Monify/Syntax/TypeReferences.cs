namespace Monify.Syntax
{
    using System;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using Attribute = System.Attribute;

    internal static class TypeReferences
    {
        private const string PreserveSuffix = "/**/";

        public static Symbol FromRoslyn(string spelling)
        {
            // Preserve Roslyn's spelling for global namespace types, arrays, tuples and nested generics.
            if (spelling.EndsWith(nameof(Attribute), StringComparison.Ordinal))
            {
                // MooVC shortens unqualified names ending in Attribute.
                spelling += PreserveSuffix;
            }

            return (Qualification)(Moniker)spelling;
        }

        public static string RemoveSpellingMarkers(this Snippet code)
        {
            return code.ToString().Replace("Attribute" + PreserveSuffix, "Attribute");
        }
    }
}