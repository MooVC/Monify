namespace Monify.Snippets.Declarations.Structs;

using Monify.Snippets.Declarations;

internal static partial class Simple
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

                    partial struct Simple
                    {
                        public Simple(int value)
                        {
                            _value = value;
                        }
                    }
                }
                """,
                Extensions.HasConstructorForEncapsulatedValue,
                "Monify.Testing.Structs.Simple.ctor");

            public static readonly Generated ConversionFromValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static implicit operator int(Simple subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            return subject._value;
                        }
                    }
                }
                """,
                Extensions.HasConversionFrom,
                "Monify.Testing.Structs.Simple.ConvertFrom");

            public static readonly Generated ConversionToValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static implicit operator Simple(int value)
                        {
                            return new Simple(value);
                        }
                    }
                }
                """,
                Extensions.HasConversionTo,
                "Monify.Testing.Structs.Simple.ConvertTo");

            public static readonly Generated EquatableForSelf = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple : IEquatable<Simple>
                    {
                    }
                }
                """,
                Extensions.HasEquatableForSelf,
                "Monify.Testing.Structs.Simple.IEquatable.Self");

            public static readonly Generated EquatableForValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple : IEquatable<int>
                    {
                    }
                }
                """,
                Extensions.HasEquatableForValue,
                "Monify.Testing.Structs.Simple.IEquatable.Value");

            public static readonly Generated ComparableInterface = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    partial struct Simple
                        : global::System.IComparable
                    {
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Interfaces.global__System_IComparable");

            public static readonly Generated ComparableGenericInterface = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    partial struct Simple
                        : global::System.IComparable<int>
                    {
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Interfaces.global__System_IComparable_int_");

            public static readonly Generated EqualityOperatorForSelf = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator ==(Simple left, Simple right)
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
                """,
                Extensions.HasEqualityOperatorForSelf,
                "Monify.Testing.Structs.Simple.Equality.Self");

            public static readonly Generated EqualityOperatorForValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator ==(Simple left, int right)
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
                """,
                Extensions.HasEqualityOperatorForValue,
                "Monify.Testing.Structs.Simple.Equality.Value");

            public static new readonly Generated Equals = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public override bool Equals(object other)
                        {
                            if (other is Simple)
                            {
                                return Equals((Simple)other);
                            }

                            return false;
                        }
                    }
                }
                """,
                Extensions.HasEqualsOverride,
                "Monify.Testing.Structs.Simple.Equals");

            public static readonly Generated FieldForEncapsulatedValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        private readonly int _value;
                    }
                }
                """,
                Extensions.HasFieldForEncapsulatedValue,
                "Monify.Testing.Structs.Simple._value");

            public static new readonly Generated GetHashCode = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public override int GetHashCode()
                        {
                            return global::Monify.Internal.HashCode.Combine(_value);
                        }
                    }
                }
                """,
                Extensions.HasGetHashCodeOverride,
                "Monify.Testing.Structs.Simple.GetHashCode");

            public static readonly Generated InequalityOperatorForSelf = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator !=(Simple left, Simple right)
                        {
                            return !(left == right);
                        }
                    }
                }
                """,
                Extensions.HasInequalityOperatorForSelf,
                "Monify.Testing.Structs.Simple.Inequality.Self");

            public static readonly Generated InequalityOperatorForValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator !=(Simple left, int right)
                        {
                            return !(left == right);
                        }
                    }
                }
                """,
                Extensions.HasInequalityOperatorForValue,
                "Monify.Testing.Structs.Simple.Inequality.Value");

            public static new readonly Generated ToString = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public override string ToString()
                        {
                            return string.Format("Simple {{ {0} }}", _value);
                        }
                    }
                }
                """,
                Extensions.HasToStringOverride,
                "Monify.Testing.Structs.Simple.ToString");

            public static readonly Generated BinaryAdditionOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator +(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value + right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_Addition.Simple-Simple");

            public static readonly Generated BinaryBitwiseAndOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator &(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value & right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_BitwiseAnd.Simple-Simple");

            public static readonly Generated BinaryBitwiseOrOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator |(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value | right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_BitwiseOr.Simple-Simple");

            public static readonly Generated BinaryDivisionOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator /(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value / right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_Division.Simple-Simple");

            public static readonly Generated BinaryExclusiveOrOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator ^(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value ^ right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_ExclusiveOr.Simple-Simple");

            public static readonly Generated BinaryGreaterThanOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator >(Simple left, Simple right)
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
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_GreaterThan.Simple-Simple");

            public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator >=(Simple left, Simple right)
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
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_GreaterThanOrEqual.Simple-Simple");

            public static readonly Generated BinaryLeftShiftOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator <<(Simple left, int right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }
                            return new Simple(left._value << right);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_LeftShift.Simple-int");

            public static readonly Generated BinaryLessThanOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator <(Simple left, Simple right)
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
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_LessThan.Simple-Simple");

            public static readonly Generated BinaryLessThanOrEqualOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static bool operator <=(Simple left, Simple right)
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
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_LessThanOrEqual.Simple-Simple");

            public static readonly Generated BinaryModulusOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator %(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value % right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_Modulus.Simple-Simple");

            public static readonly Generated BinaryMultiplyOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator *(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value * right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_Multiply.Simple-Simple");

            public static readonly Generated BinaryRightShiftOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator >>(Simple left, int right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }
                            return new Simple(left._value >> right);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_RightShift.Simple-int");

            public static readonly Generated BinarySubtractionOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator -(Simple left, Simple right)
                        {
                            if (ReferenceEquals(left, null))
                            {
                                throw new ArgumentNullException("left");
                            }

                            if (ReferenceEquals(right, null))
                            {
                                throw new ArgumentNullException("right");
                            }

                            return new Simple(left._value - right._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.BinaryOperators.op_Subtraction.Simple-Simple");

            public static readonly Generated UnaryDecrementOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator --(Simple subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            int value = subject._value;

                            return new Simple(--value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.UnaryOperators.op_Decrement.Simple");

            public static readonly Generated UnaryIncrementOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator ++(Simple subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            int value = subject._value;

                            return new Simple(++value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.UnaryOperators.op_Increment.Simple");

            public static readonly Generated UnaryOnesComplementOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator ~(Simple subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            return new Simple(~subject._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.UnaryOperators.op_OnesComplement.Simple");

            public static readonly Generated UnaryNegationOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator -(Simple subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            return new Simple(-subject._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.UnaryOperators.op_UnaryNegation.Simple");

            public static readonly Generated UnaryPlusOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public static Simple operator +(Simple subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            return new Simple(+subject._value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.UnaryOperators.op_UnaryPlus.Simple");

            public static readonly Generated EquatableToSelf = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public bool Equals(Simple other)
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
                """,
                Extensions.IsEquatableToSelf,
                "Monify.Testing.Structs.Simple.IEquatable.Self.Equals");

            public static readonly Generated EquatableToValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
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
                """,
                Extensions.IsEquatableToValue,
                "Monify.Testing.Structs.Simple.IEquatable.Value.Equals");

            public static readonly Generated CompareToInt = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public int CompareTo(int value)
                        {
                            return _value.CompareTo(value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Methods.CompareTo.int");

            public static readonly Generated CompareToObject = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    partial struct Simple
                    {
                        public int CompareTo(object value)
                        {
                            return _value.CompareTo(value);
                        }
                    }
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Methods.CompareTo.object");
        }
    }
}