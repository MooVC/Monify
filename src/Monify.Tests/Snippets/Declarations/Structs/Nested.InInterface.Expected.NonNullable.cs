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
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.00");

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
                    "Monify.Testing.Structs.IOutter.Inner.UnaryOperators.01");

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
            }
        }
    }
}