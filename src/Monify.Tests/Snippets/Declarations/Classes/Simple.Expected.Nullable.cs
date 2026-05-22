namespace Monify.Snippets.Declarations.Classes;

using Monify.Snippets.Declarations;

internal static partial class Simple
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

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.ctor");

            public static readonly Generated ConversionFromValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.ConvertFrom");

            public static readonly Generated ConversionToValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.ConvertTo");

            public static readonly Generated EquatableForSelf = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple : IEquatable<Simple>
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.HasEquatableForSelf,
                "Monify.Testing.Classes.Simple.IEquatable.Self");

            public static readonly Generated EquatableForValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple : IEquatable<int>
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.HasEquatableForValue,
                "Monify.Testing.Classes.Simple.IEquatable.Value");

            public static readonly Generated EqualityOperatorForSelf = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.Equality.Self");

            public static readonly Generated EqualityOperatorForValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.Equality.Value");

            public static new readonly Generated Equals = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.Equals");

            public static readonly Generated FieldForEncapsulatedValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
                    {
                        private readonly int _value;
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.HasFieldForEncapsulatedValue,
                "Monify.Testing.Classes.Simple._value");

            public static new readonly Generated GetHashCode = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.GetHashCode");

            public static readonly Generated InequalityOperatorForSelf = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.Inequality.Self");

            public static readonly Generated InequalityOperatorForValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.Inequality.Value");

            public static new readonly Generated ToString = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
                    {
                        public override string ToString()
                        {
                            return string.Format("Simple {{ {0} }}", _value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.HasToStringOverride,
                "Monify.Testing.Classes.Simple.ToString");

            public static readonly Generated BinaryAdditionOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_Addition.Simple-Simple");

            public static readonly Generated BinaryBitwiseAndOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_BitwiseAnd.Simple-Simple");

            public static readonly Generated BinaryBitwiseOrOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_BitwiseOr.Simple-Simple");

            public static readonly Generated BinaryDivisionOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_Division.Simple-Simple");

            public static readonly Generated BinaryExclusiveOrOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_ExclusiveOr.Simple-Simple");

            public static readonly Generated BinaryGreaterThanOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_GreaterThan.Simple-Simple");

            public static readonly Generated BinaryGreaterThanOrEqualOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_GreaterThanOrEqual.Simple-Simple");

            public static readonly Generated BinaryLeftShiftOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_LeftShift.Simple-int");

            public static readonly Generated BinaryLessThanOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_LessThan.Simple-Simple");

            public static readonly Generated BinaryLessThanOrEqualOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_LessThanOrEqual.Simple-Simple");

            public static readonly Generated BinaryModulusOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_Modulus.Simple-Simple");

            public static readonly Generated BinaryMultiplyOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_Multiply.Simple-Simple");

            public static readonly Generated BinaryRightShiftOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_RightShift.Simple-int");

            public static readonly Generated BinarySubtractionOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.BinaryOperators.op_Subtraction.Simple-Simple");

            public static readonly Generated UnaryDecrementOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.UnaryOperators.op_Decrement.Simple");

            public static readonly Generated UnaryIncrementOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.UnaryOperators.op_Increment.Simple");

            public static readonly Generated UnaryOnesComplementOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.UnaryOperators.op_OnesComplement.Simple");

            public static readonly Generated UnaryNegationOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.UnaryOperators.op_UnaryNegation.Simple");

            public static readonly Generated UnaryPlusOperator = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.UnaryOperators.op_UnaryPlus.Simple");

            public static readonly Generated EquatableToSelf = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.IEquatable.Self.Equals");

            public static readonly Generated EquatableToValue = new(
                """
                namespace Monify.Testing.Classes
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial class Simple
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
                "Monify.Testing.Classes.Simple.IEquatable.Value.Equals");
        }
    }
}