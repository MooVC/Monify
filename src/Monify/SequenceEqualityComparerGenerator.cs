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
                private const string ImmutableArrayEmptyPropertyName = "Empty";

                private static readonly ConcurrentDictionary<Type, PropertyInfo> IsDefaultProperties = new ConcurrentDictionary<Type, PropertyInfo>();
                private static readonly ConcurrentDictionary<Type, PropertyInfo> EmptyProperties = new ConcurrentDictionary<Type, PropertyInfo>();

                public bool Equals(IEnumerable left, IEnumerable right)
                {
                    if (ReferenceEquals(left, right))
                    {
                        return true;
                    }

                    if (left is null || right is null)
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
                    if (IsDefaultImmutableArray(enumerable, out IEnumerable emptyImmutableArray))
                    {
                        return emptyImmutableArray.GetEnumerator();
                    }

                    return enumerable.GetEnumerator();
                }

                private static bool IsDefaultImmutableArray(IEnumerable enumerable, out IEnumerable emptyImmutableArray)
                {
                    emptyImmutableArray = default;

                    if (!TryGetImmutableArrayElementType(enumerable, out Type elementType))
                    {
                        return false;
                    }

                    if (!TryGetIsDefault(enumerable, out bool isDefault) || !isDefault)
                    {
                        return false;
                    }

                    if (!TryGetEmptyImmutableArray(enumerable.GetType(), out IEnumerable empty))
                    {
                        emptyImmutableArray = Array.Empty<object>();

                        return true;
                    }

                    emptyImmutableArray = empty;

                    return true;
                }

                private static bool TryGetImmutableArrayElementType(IEnumerable enumerable, out Type elementType)
                {
                    elementType = default;

                    Type enumerableType = enumerable.GetType();

                    if (!(enumerableType.IsValueType && enumerableType.IsGenericType))
                    {
                        return false;
                    }

                    Type genericTypeDefinition = enumerableType.GetGenericTypeDefinition();

                    if (!string.Equals(genericTypeDefinition.FullName, ImmutableArrayGenericTypeFullName, StringComparison.Ordinal))
                    {
                        return false;
                    }

                    Type[] arguments = enumerableType.GetGenericArguments();

                    if (arguments.Length != 1)
                    {
                        return false;
                    }

                    elementType = arguments[0];

                    return true;
                }

                private static bool TryGetIsDefault(IEnumerable enumerable, out bool isDefault)
                {
                    isDefault = false;

                    Type enumerableType = enumerable.GetType();
                    PropertyInfo isDefaultProperty = GetImmutableArrayIsDefaultProperty(enumerableType);

                    if (isDefaultProperty is null)
                    {
                        return false;
                    }

                    object value = isDefaultProperty.GetValue(enumerable, default);

                    if (!(value is bool flag))
                    {
                        return false;
                    }

                    isDefault = flag;

                    return true;
                }

                private static bool TryGetEmptyImmutableArray(Type constructedImmutableArrayType, out IEnumerable emptyImmutableArray)
                {
                    emptyImmutableArray = default;

                    PropertyInfo emptyProperty = GetImmutableArrayEmptyProperty(constructedImmutableArrayType);

                    if (emptyProperty is null)
                    {
                        return false;
                    }

                    object value = emptyProperty.GetValue(default, default);

                    if (!(value is IEnumerable sequence))
                    {
                        return false;
                    }

                    emptyImmutableArray = sequence;

                    return true;
                }

                private static PropertyInfo GetImmutableArrayIsDefaultProperty(Type immutableArrayType)
                {
                    return IsDefaultProperties.GetOrAdd(
                        immutableArrayType,
                        type => type.GetProperty(ImmutableArrayIsDefaultPropertyName));
                }

                private static PropertyInfo GetImmutableArrayEmptyProperty(Type constructedImmutableArrayType)
                {
                    return EmptyProperties.GetOrAdd(
                        constructedImmutableArrayType,
                        type => type.GetProperty(ImmutableArrayEmptyPropertyName, BindingFlags.Public | BindingFlags.Static));
                }

                private static bool Equals(IEnumerator left, IEnumerator right)
                {
                    while (left.MoveNext())
                    {
                        if (!right.MoveNext())
                        {
                            return false;
                        }

                        if (!object.Equals(left.Current, right.Current))
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