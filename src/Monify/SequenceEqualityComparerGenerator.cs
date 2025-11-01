namespace Monify;

using Microsoft.CodeAnalysis;

/// <summary>
/// Generates an internal SequenceEqualityComparer static class that is used to support enumerable enumerable checks.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class SequenceEqualityComparerGenerator
    : IIncrementalGenerator
{
    /// <summary>
    /// The source code that will be output by the generator.
    /// </summary>
    public const string Content = $$"""
        namespace Monify.Internal
        {
            using System;
            using System.Collections;
            using System.Collections.Concurrent;
            using System.Collections.Generic;
            using System.Reflection;

            internal sealed class SequenceEqualityComparer
            {
                public static readonly SequenceEqualityComparer Default = new SequenceEqualityComparer();

                private const string ImmutableArrayGenericTypeFullName = "System.Collections.Immutable.ImmutableArray`1";
                private const string ImmutableArrayIsDefaultPropertyName = "IsDefault";

                private static readonly IEnumerable EmptyEnumerable = new object[0];
                private static readonly ConcurrentDictionary<Type, PropertyInfo> Properties = new ConcurrentDictionary<Type, PropertyInfo>();

                public bool Equals(IEnumerable left, IEnumerable right)
                {
                    if (ReferenceEquals(left, right))
                    {
                        return true;
                    }

                    if (ReferenceEquals(left, null) || ReferenceEquals(null, right))
                    {
                        return false;
                    }

                    IEnumerator leftEnumerator = TryGetEnumerator(left);
                    IEnumerator rightEnumerator = TryGetEnumerator(right);

                    return Equals(leftEnumerator, rightEnumerator);
                }

                public int GetHashCode(IEnumerable enumerable)
                {
                    return HashCode.Combine(enumerable);
                }

                private static IEnumerator TryGetEnumerator(IEnumerable enumerable)
                {
                    if (IsDefaultImmutableArray(enumerable))
                    {
                        return EmptyEnumerable.GetEnumerator();
                    }

                    return enumerable.GetEnumerator();
                }

                private static bool IsDefaultImmutableArray(IEnumerable enumerable)
                {
                    Type enumerableType = enumerable.GetType();

                    if (!(enumerableType.IsValueType && enumerableType.IsGenericType))
                    {
                        return false;
                    }

                    Type immutableArrayType = enumerableType.GetGenericTypeDefinition();

                    if (!string.Equals(immutableArrayType.FullName, ImmutableArrayGenericTypeFullName, StringComparison.Ordinal))
                    {
                        return false;
                    }

                    PropertyInfo isDefaultProperty = GetImmutableArrayIsDefaultProperty(enumerableType);

                    if (isDefaultProperty == null)
                    {
                        return false;
                    }

                    object isDefaultValue = isDefaultProperty.GetValue(enumerable, null);

                    return isDefaultValue is bool && (bool)isDefaultValue;
                }

                private static PropertyInfo GetImmutableArrayIsDefaultProperty(Type immutableArrayType)
                {
                    return Properties.GetOrAdd(
                        immutableArrayType,
                        immutableArrayType.GetProperty(ImmutableArrayIsDefaultPropertyName));
                }

                private static bool Equals(IEnumerator left, IEnumerator right)
                {
                    while (left.MoveNext())
                    {
                        if (!right.MoveNext())
                        {
                            return false;
                        }
        
                        if (!Equals(left.Current, right.Current))
                        {
                            return false;
                        }
                    }
        
                    return !right.MoveNext();
                }
            }
        }
        """;

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddSource("Monify.Internal.SequenceEqualityComparer.g.cs", Content));
    }
}