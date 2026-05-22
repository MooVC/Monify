namespace Monify.Snippets.Declarations.Classes;

internal static partial class Nested
{
    public static partial class InRecord
    {
        public static partial class Expected
        {
            public static class NonNullable
            {
                public static readonly Generated ConstructorForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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

                        partial record Outter<T>
                        {
                            sealed partial class Inner : IEquatable<Inner>
                            {
                            }
                        }
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

                        partial record Outter<T>
                        {
                            sealed partial class Inner : IEquatable<int>
                            {
                            }
                        }
                    }
                    """,
                    Extensions.HasEquatableForValue,
                    "Monify.Testing.Classes.Outter.Inner.IEquatable.Value");

                public static readonly Generated EqualityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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

                        partial record Outter<T>
                        {
                            sealed partial class Inner
                            {
                                private readonly int _value;
                            }
                        }
                    }
                    """,
                    Extensions.HasFieldForEncapsulatedValue,
                    "Monify.Testing.Classes.Outter.Inner._value");

                public static new readonly Generated GetHashCode = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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

                        partial record Outter<T>
                        {
                            sealed partial class Inner
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
                    "Monify.Testing.Classes.Outter.Inner.ToString");

                public static readonly Generated BinaryAdditionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_Addition.Inner-Inner");

                public static readonly Generated BinaryBitwiseAndOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_BitwiseAnd.Inner-Inner");

                public static readonly Generated BinaryBitwiseOrOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_BitwiseOr.Inner-Inner");

                public static readonly Generated BinaryDivisionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_Division.Inner-Inner");

                public static readonly Generated BinaryExclusiveOrOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_ExclusiveOr.Inner-Inner");

                public static readonly Generated BinaryGreaterThanOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_GreaterThan.Inner-Inner");

                public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_GreaterThanOrEqual.Inner-Inner");

                public static readonly Generated BinaryLeftShiftOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_LeftShift.Inner-int");

                public static readonly Generated BinaryLessThanOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_LessThan.Inner-Inner");

                public static readonly Generated BinaryLessThanOrEqualOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_LessThanOrEqual.Inner-Inner");

                public static readonly Generated BinaryModulusOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_Modulus.Inner-Inner");

                public static readonly Generated BinaryMultiplyOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_Multiply.Inner-Inner");

                public static readonly Generated BinaryRightShiftOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_RightShift.Inner-int");

                public static readonly Generated BinarySubtractionOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.BinaryOperators.op_Subtraction.Inner-Inner");

                public static readonly Generated UnaryDecrementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.UnaryOperators.op_Decrement.Inner");

                public static readonly Generated UnaryIncrementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.UnaryOperators.op_Increment.Inner");

                public static readonly Generated UnaryOnesComplementOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.UnaryOperators.op_OnesComplement.Inner");

                public static readonly Generated UnaryNegationOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.UnaryOperators.op_UnaryNegation.Inner");

                public static readonly Generated UnaryPlusOperator = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Classes.Outter.Inner.UnaryOperators.op_UnaryPlus.Inner");

                public static readonly Generated EquatableToSelf = new(
                    """
                    namespace Monify.Testing.Classes
                    {
                        using System;
                        using System.Collections.Generic;

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
                    }
                    """,
                    Extensions.IsEquatableToValue,
                    "Monify.Testing.Classes.Outter.Inner.IEquatable.Value.Equals");
            }
        }
    }
}