namespace Monify.Snippets.Declarations.Structs;

internal static partial class Simple
{
    public static partial class Expected
    {
        public static class Nullable
        {
            public static readonly Generated ConstructorForEncapsulatedValue = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public Simple(int value)
                        {
                            _value = value;
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public static implicit operator Simple(int value)
                        {
                            return new Simple(value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple : IEquatable<Simple>
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple : IEquatable<int>
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    partial struct Simple
                        : global::System.IComparable
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Interfaces.globalSystemIComparable");

            public static readonly Generated ComparableGenericInterface = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
                    partial struct Simple
                        : global::System.IComparable<int>
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Interfaces.globalSystemIComparableint");

            public static readonly Generated EqualityOperatorForSelf = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        private readonly int _value;
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.HasFieldForEncapsulatedValue,
                "Monify.Testing.Structs.Simple._value");

            public static readonly Generated DebuggerDisplay = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    [global::System.Diagnostics.DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
                    partial struct Simple
                    {
                        private string GetDebuggerDisplay()
                        {
                            return string.Format("Simple {{ {0} }}", _value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.DebuggerDisplay");

            public static new readonly Generated GetHashCode = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public override int GetHashCode()
                        {
                            return global::Monify.Internal.HashCode.Combine(_value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public static bool operator !=(Simple left, Simple right)
                        {
                            return !(left == right);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public static bool operator !=(Simple left, int right)
                        {
                            return !(left == right);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_Addition.Simple-Simple");

            public static readonly Generated BinaryBitwiseAndOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_BitwiseAnd.Simple-Simple");

            public static readonly Generated BinaryBitwiseOrOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_BitwiseOr.Simple-Simple");

            public static readonly Generated BinaryDivisionOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_Division.Simple-Simple");

            public static readonly Generated BinaryExclusiveOrOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_ExclusiveOr.Simple-Simple");

            public static readonly Generated BinaryGreaterThanOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_GreaterThan.Simple-Simple");

            public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_GreaterThanOrEqual.Simple-Simple");

            public static readonly Generated BinaryLeftShiftOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_LeftShift.Simple-int");

            public static readonly Generated BinaryLessThanOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_LessThan.Simple-Simple");

            public static readonly Generated BinaryLessThanOrEqualOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_LessThanOrEqual.Simple-Simple");

            public static readonly Generated BinaryModulusOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_Modulus.Simple-Simple");

            public static readonly Generated BinaryMultiplyOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_Multiply.Simple-Simple");

            public static readonly Generated BinaryRightShiftOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_RightShift.Simple-int");

            public static readonly Generated BinarySubtractionOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Binary.op_Subtraction.Simple-Simple");

            public static readonly Generated UnaryDecrementOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Unary.op_Decrement.Simple");

            public static readonly Generated UnaryIncrementOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Unary.op_Increment.Simple");

            public static readonly Generated UnaryOnesComplementOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Unary.op_OnesComplement.Simple");

            public static readonly Generated UnaryNegationOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Unary.op_UnaryNegation.Simple");

            public static readonly Generated UnaryPlusOperator = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Unary.op_UnaryPlus.Simple");

            public static readonly Generated EquatableToSelf = new(
                """
                namespace Monify.Testing.Structs
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public int CompareTo(int value)
                        {
                            return _value.CompareTo(value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    partial struct Simple
                    {
                        public int CompareTo(object value)
                        {
                            if (value is Simple)
                            {
                                value = ((Simple)value)._value;
                            }

                            return _value.CompareTo(value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.None,
                "Monify.Testing.Structs.Simple.Methods.CompareTo.object");
        }
    }
}