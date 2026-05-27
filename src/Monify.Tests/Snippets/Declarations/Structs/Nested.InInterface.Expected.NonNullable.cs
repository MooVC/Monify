namespace Monify.Snippets.Declarations.Structs;

internal static partial class Nested
{
    public static partial class InInterface
    {
        public static partial class Expected
        {
            public static class NonNullable
            {
                public static readonly Generated ConstructorForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public Inner(int value)
                                {
                                    _value = value;
                                }
                            }
                        }
                    }
                    """,
                    Extensions.HasConstructorForEncapsulatedValue,
                    "Monify.Testing.Structs.IOutter.Inner.ctor");

                public static readonly Generated ConversionFromValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.HasConversionFrom,
                    "Monify.Testing.Structs.IOutter.Inner.ConvertFrom");

                public static readonly Generated ConversionToValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public static implicit operator Inner(int value)
                                {
                                    return new Inner(value);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.HasConversionTo,
                    "Monify.Testing.Structs.IOutter.Inner.ConvertTo");

                public static readonly Generated EquatableForSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner : IEquatable<Inner>
                            {
                            }
                        }
                    }
                    """,
                    Extensions.HasEquatableForSelf,
                    "Monify.Testing.Structs.IOutter.Inner.IEquatable.Self");

                public static readonly Generated EquatableForValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner : IEquatable<int>
                            {
                            }
                        }
                    }
                    """,
                    Extensions.HasEquatableForValue,
                    "Monify.Testing.Structs.IOutter.Inner.IEquatable.Value");

                public static readonly Generated ComparableInterface = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            readonly partial struct Inner
                                : global::System.IComparable
                            {
                            }
                        }
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.Interfaces.global__System_IComparable");

                public static readonly Generated ComparableGenericInterface = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                            readonly partial struct Inner
                                : global::System.IComparable<int>
                            {
                            }
                        }
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.Interfaces.global__System_IComparable_int_");

                public static readonly Generated EqualityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.HasEqualityOperatorForSelf,
                    "Monify.Testing.Structs.IOutter.Inner.Equality.Self");

                public static readonly Generated EqualityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.HasEqualityOperatorForValue,
                    "Monify.Testing.Structs.IOutter.Inner.Equality.Value");

                public static new readonly Generated Equals = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.HasEqualsOverride,
                    "Monify.Testing.Structs.IOutter.Inner.Equals");

                public static readonly Generated FieldForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                private readonly int _value;
                            }
                        }
                    }
                    """,
                    Extensions.HasFieldForEncapsulatedValue,
                    "Monify.Testing.Structs.IOutter.Inner._value");

                public static new readonly Generated GetHashCode = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public override int GetHashCode()
                                {
                                    return global::Monify.Internal.HashCode.Combine(_value);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.HasGetHashCodeOverride,
                    "Monify.Testing.Structs.IOutter.Inner.GetHashCode");

                public static readonly Generated InequalityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public static bool operator !=(Inner left, Inner right)
                                {
                                    return !(left == right);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.HasInequalityOperatorForSelf,
                    "Monify.Testing.Structs.IOutter.Inner.Inequality.Self");

                public static readonly Generated InequalityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public static bool operator !=(Inner left, int right)
                                {
                                    return !(left == right);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.HasInequalityOperatorForValue,
                    "Monify.Testing.Structs.IOutter.Inner.Inequality.Value");

                public static new readonly Generated ToString = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public override string ToString()
                                {
                                    return string.Format("Inner {{ {0} }}", _value);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.HasToStringOverride,
                    "Monify.Testing.Structs.IOutter.Inner.ToString");

                public static readonly Generated BinaryAdditionOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_Addition.Inner-Inner");

                public static readonly Generated BinaryBitwiseAndOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_BitwiseAnd.Inner-Inner");

                public static readonly Generated BinaryBitwiseOrOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_BitwiseOr.Inner-Inner");

                public static readonly Generated BinaryDivisionOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_Division.Inner-Inner");

                public static readonly Generated BinaryExclusiveOrOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_ExclusiveOr.Inner-Inner");

                public static readonly Generated BinaryGreaterThanOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_GreaterThan.Inner-Inner");

                public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_GreaterThanOrEqual.Inner-Inner");

                public static readonly Generated BinaryLeftShiftOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_LeftShift.Inner-int");

                public static readonly Generated BinaryLessThanOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_LessThan.Inner-Inner");

                public static readonly Generated BinaryLessThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_LessThanOrEqual.Inner-Inner");

                public static readonly Generated BinaryModulusOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_Modulus.Inner-Inner");

                public static readonly Generated BinaryMultiplyOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_Multiply.Inner-Inner");

                public static readonly Generated BinaryRightShiftOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_RightShift.Inner-int");

                public static readonly Generated BinarySubtractionOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.BinaryOperators.op_Subtraction.Inner-Inner");

                public static readonly Generated UnaryDecrementOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.op_Decrement.Inner");

                public static readonly Generated UnaryIncrementOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.op_Increment.Inner");

                public static readonly Generated UnaryOnesComplementOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.op_OnesComplement.Inner");

                public static readonly Generated UnaryNegationOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.op_UnaryNegation.Inner");

                public static readonly Generated UnaryPlusOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.op_UnaryPlus.Inner");

                public static readonly Generated EquatableToSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.IsEquatableToSelf,
                    "Monify.Testing.Structs.IOutter.Inner.IEquatable.Self.Equals");

                public static readonly Generated EquatableToValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
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
                    }
                    """,
                    Extensions.IsEquatableToValue,
                    "Monify.Testing.Structs.IOutter.Inner.IEquatable.Value.Equals");

                public static readonly Generated CompareToInt = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public int CompareTo(int value)
                                {
                                    return _value.CompareTo(value);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.Methods.CompareTo.int");

                public static readonly Generated CompareToObject = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        partial interface IOutter<T>
                        {
                            readonly partial struct Inner
                            {
                                public int CompareTo(object value)
                                {
                                    return _value.CompareTo(value);
                                }
                            }
                        }
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.IOutter.Inner.Methods.CompareTo.object");
            }
        }
    }
}