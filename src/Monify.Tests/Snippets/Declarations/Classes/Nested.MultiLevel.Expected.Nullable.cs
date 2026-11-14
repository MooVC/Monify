namespace Monify.Snippets.Declarations.Classes;

internal static partial class Nested
{
    public static partial class MultiLevel
    {
        public static partial class Expected
        {
            public static class Nullable
            {
                public static readonly Generated ConstructorForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public InlineStyle(int value)
                                    {
                                        _value = value;
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConstructorForEncapsulatedValue,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.ctor");

                public static readonly Generated JsonConverter = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                [global::System.Text.Json.Serialization.JsonConverter(typeof(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Converter))]
                                sealed partial class InlineStyle
                                {
                                    public sealed class Converter
                                        : global::System.Text.Json.Serialization.JsonConverter<global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle>
                                    {
                                        public override global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle Read(ref global::System.Text.Json.Utf8JsonReader reader, global::System.Type typeToConvert, global::System.Text.Json.JsonSerializerOptions options)
                                        {
                                            return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(global::System.Text.Json.JsonSerializer.Deserialize<int>(ref reader, options));
                                        }

                                        public override void Write(global::System.Text.Json.Utf8JsonWriter writer, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle value, global::System.Text.Json.JsonSerializerOptions options)
                                        {
                                            global::System.Text.Json.JsonSerializer.Serialize<int>(writer, value._value, options);
                                        }
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.JsonConverter",
                    RequiresJsonSerialization: true);

                public static readonly Generated ConversionFromValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static implicit operator int(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle subject)
                                    {
                                        if (global::System.Object.ReferenceEquals(subject, null))
                                        {
                                            throw new global::System.ArgumentNullException("subject");
                                        }

                                        return subject._value;
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConversionFrom,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.ConvertFrom");

                public static readonly Generated ConversionToValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static implicit operator global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(int value)
                                    {
                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConversionTo,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.ConvertTo");

                public static readonly Generated EquatableForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                    : global::System.IEquatable<global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle>
                                {
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEquatableForSelf,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.IEquatable.Self");

                public static readonly Generated EquatableForValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                    : global::System.IEquatable<int>
                                {
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEquatableForValue,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.IEquatable.Value");

                public static readonly Generated ComparableInterface = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                                sealed partial class InlineStyle
                                    : global::System.IComparable
                                {
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Interfaces.globalSystemIComparable");

                public static readonly Generated ComparableGenericInterface = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                                [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                                sealed partial class InlineStyle
                                    : global::System.IComparable<int>
                                {
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Interfaces.globalSystemIComparableint");

                public static readonly Generated EqualityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator ==(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, right))
                                        {
                                            return true;
                                        }

                                        if (global::System.Object.ReferenceEquals(left, null) || global::System.Object.ReferenceEquals(right, null))
                                        {
                                            return false;
                                        }

                                        return left.Equals(right);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualityOperatorForSelf,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Equality.Self");

                public static readonly Generated EqualityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator ==(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, int right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, right))
                                        {
                                            return true;
                                        }

                                        if (global::System.Object.ReferenceEquals(left, null) || global::System.Object.ReferenceEquals(right, null))
                                        {
                                            return false;
                                        }

                                        return left.Equals(right);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualityOperatorForValue,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Equality.Value");

                public static readonly Generated EqualsOverride = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public override bool Equals(object other)
                                    {
                                        if (other is global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle)
                                        {
                                            return Equals((global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle)other);
                                        }

                                        return false;
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualsOverride,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Equals");

                public static readonly Generated FieldForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    private readonly int _value;
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasFieldForEncapsulatedValue,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle._value");

                public static readonly Generated DebuggerDisplay = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                [global::System.Diagnostics.DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
                                sealed partial class InlineStyle
                                {
                                    private string GetDebuggerDisplay()
                                    {
                                        return string.Format("InlineStyle {{ {0} }}", _value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.DebuggerDisplay");

                public static new readonly Generated GetHashCode = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public override int GetHashCode()
                                    {
                                        return global::Monify.Internal.HashCode.Combine(_value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasGetHashCodeOverride,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.GetHashCode");

                public static readonly Generated InequalityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator !=(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        return !(left == right);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasInequalityOperatorForSelf,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Inequality.Self");

                public static readonly Generated InequalityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator !=(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, int right)
                                    {
                                        return !(left == right);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasInequalityOperatorForValue,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Inequality.Value");

                public static new readonly Generated ToString = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public override string ToString()
                                    {
                                        if (global::System.Object.ReferenceEquals(_value, null))
                                        {
                                            return string.Empty;
                                        }

                                        return _value.ToString();
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasToStringOverride,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.ToString");

                public static readonly Generated BinaryAdditionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator +(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value + right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_Addition.InlineStyle-InlineStyle");

                public static readonly Generated BinaryBitwiseAndOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator &(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value & right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_BitwiseAnd.InlineStyle-InlineStyle");

                public static readonly Generated BinaryBitwiseOrOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator |(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value | right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_BitwiseOr.InlineStyle-InlineStyle");

                public static readonly Generated BinaryDivisionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator /(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value / right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_Division.InlineStyle-InlineStyle");

                public static readonly Generated BinaryExclusiveOrOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator ^(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value ^ right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_ExclusiveOr.InlineStyle-InlineStyle");

                public static readonly Generated BinaryGreaterThanOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator >(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return (bool)(left._value > right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_GreaterThan.InlineStyle-InlineStyle");

                public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator >=(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return (bool)(left._value >= right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_GreaterThanOrEqual.InlineStyle-InlineStyle");

                public static readonly Generated BinaryLeftShiftOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator <<(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, int right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value << right);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_LeftShift.InlineStyle-int");

                public static readonly Generated BinaryLessThanOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator <(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return (bool)(left._value < right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_LessThan.InlineStyle-InlineStyle");

                public static readonly Generated BinaryLessThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static bool operator <=(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return (bool)(left._value <= right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_LessThanOrEqual.InlineStyle-InlineStyle");

                public static readonly Generated BinaryModulusOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator %(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value % right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_Modulus.InlineStyle-InlineStyle");

                public static readonly Generated BinaryMultiplyOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator *(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value * right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_Multiply.InlineStyle-InlineStyle");

                public static readonly Generated BinaryRightShiftOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator >>(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, int right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value >> right);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_RightShift.InlineStyle-int");

                public static readonly Generated BinarySubtractionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator -(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle left, global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle right)
                                    {
                                        if (global::System.Object.ReferenceEquals(left, null))
                                        {
                                            throw new global::System.ArgumentNullException("left");
                                        }

                                        if (global::System.Object.ReferenceEquals(right, null))
                                        {
                                            throw new global::System.ArgumentNullException("right");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(left._value - right._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Binary.op_Subtraction.InlineStyle-InlineStyle");

                public static readonly Generated UnaryDecrementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator --(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle subject)
                                    {
                                        if (global::System.Object.ReferenceEquals(subject, null))
                                        {
                                            throw new global::System.ArgumentNullException("subject");
                                        }

                                        int value = subject._value;

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(--value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Unary.op_Decrement.InlineStyle");

                public static readonly Generated UnaryIncrementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator ++(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle subject)
                                    {
                                        if (global::System.Object.ReferenceEquals(subject, null))
                                        {
                                            throw new global::System.ArgumentNullException("subject");
                                        }

                                        int value = subject._value;

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(++value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Unary.op_Increment.InlineStyle");

                public static readonly Generated UnaryOnesComplementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator ~(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle subject)
                                    {
                                        if (global::System.Object.ReferenceEquals(subject, null))
                                        {
                                            throw new global::System.ArgumentNullException("subject");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(~subject._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Unary.op_OnesComplement.InlineStyle");

                public static readonly Generated UnaryNegationOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator -(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle subject)
                                    {
                                        if (global::System.Object.ReferenceEquals(subject, null))
                                        {
                                            throw new global::System.ArgumentNullException("subject");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(-subject._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Unary.op_UnaryNegation.InlineStyle");

                public static readonly Generated UnaryPlusOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public static global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle operator +(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle subject)
                                    {
                                        if (global::System.Object.ReferenceEquals(subject, null))
                                        {
                                            throw new global::System.ArgumentNullException("subject");
                                        }

                                        return new global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle(+subject._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Unary.op_UnaryPlus.InlineStyle");

                public static readonly Generated EquatableToSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public bool Equals(global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle other)
                                    {
                                        if (global::System.Object.ReferenceEquals(this, other))
                                        {
                                            return true;
                                        }

                                        if (global::System.Object.ReferenceEquals(other, null))
                                        {
                                            return false;
                                        }

                                        return Equals(other._value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.IsEquatableToSelf,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.IEquatable.Self.Equals");

                public static readonly Generated EquatableToValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public bool Equals(int other)
                                    {
                                        if (global::System.Object.ReferenceEquals(this, other))
                                        {
                                            return true;
                                        }

                                        if (global::System.Object.ReferenceEquals(other, null))
                                        {
                                            return false;
                                        }

                                        return other.Equals(_value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.IsEquatableToValue,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.IEquatable.Value.Equals");

                public static readonly Generated CompareToInt = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public int CompareTo(int value)
                                    {
                                        return _value.CompareTo(value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Methods.CompareTo.int");

                public static readonly Generated CompareToObject = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        #nullable disable
                        #pragma warning disable CS8625

                        partial class Snippet
                        {
                            partial class BlockOptions
                            {
                                sealed partial class InlineStyle
                                {
                                    public int CompareTo(object value)
                                    {
                                        if (value is global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle)
                                        {
                                            value = ((global::Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle)value)._value;
                                        }

                                        return _value.CompareTo(value);
                                    }
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Snippet.BlockOptions.InlineStyle.Methods.CompareTo.object");
            }
        }
    }
}