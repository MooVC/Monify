namespace Monify.Strategies;

using System.Text;

internal static class StringExtensions
{
    public static string NormalizeTypeForHint(this string value)
    {
        var builder = new StringBuilder(value.Length);

        foreach (char character in value)
        {
            _ = char.IsLetterOrDigit(character)
                ? builder.Append(character)
                : builder.Append('_');
        }

        return builder.ToString();
    }
}