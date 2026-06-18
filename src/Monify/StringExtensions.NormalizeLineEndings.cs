namespace Monify
{
    /// <summary>
    /// Provides extensions relating to <see langword="string"/>.
    /// </summary>
    internal static partial class StringExtensions
    {
        /// <summary>
        /// Normalizes all line endings to line feeds.
        /// </summary>
        /// <param name="input">
        /// The input string to process.
        /// </param>
        /// <returns>
        /// A new string with all line endings normalized to line feeds.
        /// </returns>
        public static string NormalizeLineEndings(this string input)
        {
            return input
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");
        }
    }
}