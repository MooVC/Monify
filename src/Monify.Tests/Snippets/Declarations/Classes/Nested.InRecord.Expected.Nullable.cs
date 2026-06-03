namespace Monify.Snippets.Declarations.Classes;

internal static partial class Nested
{
    public static partial class InRecord
    {
        public static partial class Expected
        {
            public static class Nullable
            {
                public static readonly Generated ConstructorForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public Inner(int value)
                                {
                                    _value = value;
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConstructorForEncapsulatedValue,
                    "Monify.Testing.Classes.Outter.Inner.ctor");

                public static readonly Generated ConversionFromValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static implicit operator int(Inner subject)
                                {
                                    if (ReferenceEquals(subject, null))
                                    {
                                        throw new ArgumentNullException("subject");
                                    }

                                    return subject._value;
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConversionFrom,
                    "Monify.Testing.Classes.Outter.Inner.ConvertFrom");

                public static readonly Generated ConversionToValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static implicit operator Inner(int value)
                                {
                                    return new Inner(value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConversionTo,
                    "Monify.Testing.Classes.Outter.Inner.ConvertTo");

                public static readonly Generated EquatableForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner : IEquatable<Inner>
                            {
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEquatableForSelf,
                    "Monify.Testing.Classes.Outter.Inner.IEquatable.Self");

                public static readonly Generated EquatableForValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner : IEquatable<int>
                            {
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEquatableForValue,
                    "Monify.Testing.Classes.Outter.Inner.IEquatable.Value");

                public static readonly Generated ComparableInterface = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            sealed partial class Inner
                                : global::System.IComparable
                            {
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Interfaces.globalSystemIComparable");

                public static readonly Generated ComparableGenericInterface = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            sealed partial class Inner
                                : global::System.IComparable<int>
                            {
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Interfaces.globalSystemIComparableint");

                public static readonly Generated EqualityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator ==(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, right))
                                    {
                                        return true;
                                    }

                                    if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                                    {
                                        return false;
                                    }

                                    return left.Equals(right);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualityOperatorForSelf,
                    "Monify.Testing.Classes.Outter.Inner.Equality.Self");

                public static readonly Generated EqualityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator ==(Inner left, int right)
                                {
                                    if (ReferenceEquals(left, right))
                                    {
                                        return true;
                                    }

                                    if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                                    {
                                        return false;
                                    }

                                    return left.Equals(right);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualityOperatorForValue,
                    "Monify.Testing.Classes.Outter.Inner.Equality.Value");

                public static new readonly Generated Equals = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public override bool Equals(object other)
                                {
                                    if (other is Inner)
                                    {
                                        return Equals((Inner)other);
                                    }

                                    return false;
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualsOverride,
                    "Monify.Testing.Classes.Outter.Inner.Equals");

                public static readonly Generated FieldForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                private readonly int _value;
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasFieldForEncapsulatedValue,
                    "Monify.Testing.Classes.Outter.Inner._value");

                public static readonly Generated DebuggerDisplay = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            [global::System.Diagnostics.DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
                            sealed partial class Inner
                            {
                                private string GetDebuggerDisplay()
                                {
                                    return string.Format("Inner {{ {0} }}", _value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.DebuggerDisplay");

                public static new readonly Generated GetHashCode = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public override int GetHashCode()
                                {
                                    return global::Monify.Internal.HashCode.Combine(_value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasGetHashCodeOverride,
                    "Monify.Testing.Classes.Outter.Inner.GetHashCode");

                public static readonly Generated InequalityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator !=(Inner left, Inner right)
                                {
                                    return !(left == right);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasInequalityOperatorForSelf,
                    "Monify.Testing.Classes.Outter.Inner.Inequality.Self");

                public static readonly Generated InequalityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator !=(Inner left, int right)
                                {
                                    return !(left == right);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasInequalityOperatorForValue,
                    "Monify.Testing.Classes.Outter.Inner.Inequality.Value");

                public static new readonly Generated ToString = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public override string ToString()
                                {
                                    if (ReferenceEquals(_value, null))
                                    {
                                        return string.Empty;
                                    }

                                    return _value.ToString();
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasToStringOverride,
                    "Monify.Testing.Classes.Outter.Inner.ToString");

                public static readonly Generated BinaryAdditionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator +(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value + right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_Addition.Inner-Inner");

                public static readonly Generated BinaryBitwiseAndOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator &(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value & right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_BitwiseAnd.Inner-Inner");

                public static readonly Generated BinaryBitwiseOrOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator |(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value | right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_BitwiseOr.Inner-Inner");

                public static readonly Generated BinaryDivisionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator /(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value / right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_Division.Inner-Inner");

                public static readonly Generated BinaryExclusiveOrOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator ^(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value ^ right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_ExclusiveOr.Inner-Inner");

                public static readonly Generated BinaryGreaterThanOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator >(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return (bool)(left._value > right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_GreaterThan.Inner-Inner");

                public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator >=(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return (bool)(left._value >= right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_GreaterThanOrEqual.Inner-Inner");

                public static readonly Generated BinaryLeftShiftOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator <<(Inner left, int right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    return new Inner(left._value << right);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_LeftShift.Inner-int");

                public static readonly Generated BinaryLessThanOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator <(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return (bool)(left._value < right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_LessThan.Inner-Inner");

                public static readonly Generated BinaryLessThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static bool operator <=(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return (bool)(left._value <= right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_LessThanOrEqual.Inner-Inner");

                public static readonly Generated BinaryModulusOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator %(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value % right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_Modulus.Inner-Inner");

                public static readonly Generated BinaryMultiplyOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator *(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value * right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_Multiply.Inner-Inner");

                public static readonly Generated BinaryRightShiftOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator >>(Inner left, int right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    return new Inner(left._value >> right);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_RightShift.Inner-int");

                public static readonly Generated BinarySubtractionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator -(Inner left, Inner right)
                                {
                                    if (ReferenceEquals(left, null))
                                    {
                                        throw new ArgumentNullException("left");
                                    }

                                    if (ReferenceEquals(right, null))
                                    {
                                        throw new ArgumentNullException("right");
                                    }

                                    return new Inner(left._value - right._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Binary.op_Subtraction.Inner-Inner");

                public static readonly Generated UnaryDecrementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator --(Inner subject)
                                {
                                    if (ReferenceEquals(subject, null))
                                    {
                                        throw new ArgumentNullException("subject");
                                    }

                                    int value = subject._value;

                                    return new Inner(--value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Unary.op_Decrement.Inner");

                public static readonly Generated UnaryIncrementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator ++(Inner subject)
                                {
                                    if (ReferenceEquals(subject, null))
                                    {
                                        throw new ArgumentNullException("subject");
                                    }

                                    int value = subject._value;

                                    return new Inner(++value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Unary.op_Increment.Inner");

                public static readonly Generated UnaryOnesComplementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator ~(Inner subject)
                                {
                                    if (ReferenceEquals(subject, null))
                                    {
                                        throw new ArgumentNullException("subject");
                                    }

                                    return new Inner(~subject._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Unary.op_OnesComplement.Inner");

                public static readonly Generated UnaryNegationOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator -(Inner subject)
                                {
                                    if (ReferenceEquals(subject, null))
                                    {
                                        throw new ArgumentNullException("subject");
                                    }

                                    return new Inner(-subject._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Unary.op_UnaryNegation.Inner");

                public static readonly Generated UnaryPlusOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public static Inner operator +(Inner subject)
                                {
                                    if (ReferenceEquals(subject, null))
                                    {
                                        throw new ArgumentNullException("subject");
                                    }

                                    return new Inner(+subject._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Unary.op_UnaryPlus.Inner");

                public static readonly Generated EquatableToSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public bool Equals(Inner other)
                                {
                                    if (ReferenceEquals(this, other))
                                    {
                                        return true;
                                    }

                                    if (ReferenceEquals(other, null))
                                    {
                                        return false;
                                    }

                                    return Equals(other._value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.IsEquatableToSelf,
                    "Monify.Testing.Classes.Outter.Inner.IEquatable.Self.Equals");

                public static readonly Generated EquatableToValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public bool Equals(int other)
                                {
                                    if (ReferenceEquals(this, other))
                                    {
                                        return true;
                                    }

                                    if (ReferenceEquals(other, null))
                                    {
                                        return false;
                                    }

                                    return global::System.Collections.Generic.EqualityComparer<int>.Default.Equals(_value, other);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.IsEquatableToValue,
                    "Monify.Testing.Classes.Outter.Inner.IEquatable.Value.Equals");

                public static readonly Generated CompareToInt = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public int CompareTo(int value)
                                {
                                    return _value.CompareTo(value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Methods.CompareTo.int");

                public static readonly Generated CompareToObject = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                public int CompareTo(object value)
                                {
                                    if (value is Inner)
                                    {
                                        value = ((Inner)value)._value;
                                    }

                                    return _value.CompareTo(value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.Methods.CompareTo.object");
            }
        }
    }
}