namespace Monify.Snippets.Declarations.Records;

internal static partial class Nested
{
    public static partial class InClass
    {
        public static partial class Expected
        {
            public static class NonNullable
            {
                public static readonly Generated ConstructorForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.ctor");

                public static readonly Generated ConversionFromValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.ConvertFrom");

                public static readonly Generated ConversionToValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.ConvertTo");

                public static readonly Generated EquatableForSelf = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner : IEquatable<Inner>
                            {
                            }
                        }
                    }
                    """,
                    Extensions.HasEquatableForSelf,
                    "Monify.Testing.Records.Outter.Inner.IEquatable.Self");

                public static readonly Generated EquatableForValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner : IEquatable<int>
                            {
                            }
                        }
                    }
                    """,
                    Extensions.HasEquatableForValue,
                    "Monify.Testing.Records.Outter.Inner.IEquatable.Value");

                public static readonly Generated EqualityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.Equality.Self");

                public static readonly Generated EqualityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.Equality.Value");

                public static new readonly Generated Equals = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.Equals");

                public static readonly Generated FieldForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
                            {
                                private readonly int _value;
                            }
                        }
                    }
                    """,
                    Extensions.HasFieldForEncapsulatedValue,
                    "Monify.Testing.Records.Outter.Inner._value");

                public static new readonly Generated GetHashCode = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.GetHashCode");

                public static readonly Generated InequalityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.Inequality.Self");

                public static readonly Generated InequalityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.Inequality.Value");

                public static new readonly Generated ToString = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.ToString");

                public static readonly Generated UnaryNegationOperator = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.UnaryOperators.00");

                public static readonly Generated UnaryPlusOperator = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.UnaryOperators.01");

                public static readonly Generated EquatableToSelf = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.IEquatable.Self.Equals");

                public static readonly Generated EquatableToValue = new(
                    """
                    namespace Monify.Testing.Records
                    {
                        using System;
                        using System.Collections.Generic;

                        partial class Outter<T>
                        {
                            sealed partial record Inner
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
                    "Monify.Testing.Records.Outter.Inner.IEquatable.Value.Equals");
            }
        }
    }
}