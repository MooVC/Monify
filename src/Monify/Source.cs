namespace Monify
{
    using Monify.Syntax;
    using MooVC.Syntax;

    /// <summary>
    /// Represents a unit of source code to be added to the output.
    /// </summary>
    internal sealed class Source
    {
        /// <summary>
        /// Constructs an instance of representing a unit of source code to be added to the output.
        /// </summary>
        /// <param name="code">
        /// The contents of the source file.
        /// </param>
        /// <param name="hint">
        /// The hint name to be used for the source file.
        /// </param>
        /// <param name="isNested">
        /// A value indicating whether the source should be enclosed in the subject's containing types.
        /// </param>
        public Source(Snippet code, string hint, bool isNested = true)
        {
            Content = code;
            Hint = hint;
            IsNested = isNested;
        }

        /// <summary>
        /// Gets the contents of the source file.
        /// </summary>
        /// <value>
        /// The contents of the source file.
        /// </value>
        public string Code => Content.RemoveSpellingMarkers().NormalizeLineEndings();

        public Snippet Content { get; }

        /// <summary>
        /// Gets the hint name to be used for the source file.
        /// </summary>
        /// <value>
        /// The hint name to be used for the source file.
        /// </value>
        public string Hint { get; }

        /// <summary>
        /// Gets a value indicating whether the source should be enclosed in the subject's containing types.
        /// </summary>
        /// <value>
        /// A value indicating whether the source should be enclosed in the subject's containing types.
        /// </value>
        public bool IsNested { get; }
    }
}