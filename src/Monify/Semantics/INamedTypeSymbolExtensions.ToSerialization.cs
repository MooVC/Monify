namespace Monify.Semantics
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Monify.Model;

    /// <summary>
    /// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
    /// </summary>
    internal static partial class INamedTypeSymbolExtensions
    {
        private const string FullyQualifiedMetadataName = "System.Text.Json.Serialization.JsonConverter`1";

        /// <summary>
        /// Maps serialization metadata from the <paramref name="subject"/> into a <see cref="Serialization"/> instance.
        /// </summary>
        /// <param name="subject">
        /// The subject from which serialization metadata is identified.
        /// </param>
        /// <param name="compilation">
        /// The compilation used to determine whether JSON serialization is available.
        /// </param>
        /// <returns>
        /// The serialization metadata associated with the subject.
        /// </returns>
        public static Serialization ToSerialization(this INamedTypeSymbol subject, Compilation compilation)
        {
            string factoryName = string.Empty;
            string factoryTarget = string.Empty;

            if (subject.IsGenericType)
            {
                factoryName = GetJsonConverterFactoryName(subject);
                factoryTarget = GetJsonConverterFactoryTarget(subject);
            }

            bool supportsSerialization = !subject.IsRefLikeType
                && compilation.GetTypeByMetadataName(FullyQualifiedMetadataName) is object;

            return new Serialization()
                .HasJsonConverter(subject.HasJsonConverter())
                .WithJsonConverterFactoryName(factoryName)
                .WithJsonConverterFactoryTarget(factoryTarget)
                .SupportsJsonSerialization(supportsSerialization);
        }

        private static string GetJsonConverterFactoryName(INamedTypeSymbol subject)
        {
            var names = new Stack<string>();

            while (subject is object)
            {
                names.Push(subject.MetadataName.Replace("`", "_"));
                subject = subject.ContainingType;
            }

            return string.Join("_", names) + "JsonConverterFactory";
        }

        private static string GetJsonConverterFactoryTarget(INamedTypeSymbol subject)
        {
            string prefix = subject.ContainingNamespace.IsGlobalNamespace
                ? string.Empty
                : subject.ContainingNamespace.ToDisplayString() + ".";

            var names = new Stack<string>();

            while (subject is object)
            {
                names.Push(subject.MetadataName);
                subject = subject.ContainingType;
            }

            return prefix + string.Join("+", names);
        }
    }
}