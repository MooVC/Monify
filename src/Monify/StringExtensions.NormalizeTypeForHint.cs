namespace Monify
{
    using System.Text;

    internal static partial class StringExtensions
    {
        public static string NormalizeTypeForHint(this string value)
        {
            var builder = new StringBuilder(value.Length);

            foreach (char character in value)
            {
                _ = char.IsLetterOrDigit(character)
                    ? builder.Append(character)
                    : builder.Append(string.Empty);
            }

            return builder.ToString();
        }
    }
}