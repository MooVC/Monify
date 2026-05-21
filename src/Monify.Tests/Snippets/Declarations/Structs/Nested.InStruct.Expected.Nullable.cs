namespace Monify.Snippets.Declarations.Structs;

internal static partial class Nested
{
    public static partial class InStruct
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

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
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
                    "Monify.Testing.Structs.Outter.Inner.ctor");

                public static readonly Generated ConversionFromValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasConversionFrom,
                    "Monify.Testing.Structs.Outter.Inner.ConvertFrom");

                public static readonly Generated ConversionToValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
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
                    "Monify.Testing.Structs.Outter.Inner.ConvertTo");

                public static readonly Generated EquatableForSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner : IEquatable<Inner>
                            {
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEquatableForSelf,
                    "Monify.Testing.Structs.Outter.Inner.IEquatable.Self");

                public static readonly Generated EquatableForValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner : IEquatable<int>
                            {
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEquatableForValue,
                    "Monify.Testing.Structs.Outter.Inner.IEquatable.Value");

                public static readonly Generated EqualityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualityOperatorForSelf,
                    "Monify.Testing.Structs.Outter.Inner.Equality.Self");

                public static readonly Generated EqualityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualityOperatorForValue,
                    "Monify.Testing.Structs.Outter.Inner.Equality.Value");

                public static new readonly Generated Equals = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasEqualsOverride,
                    "Monify.Testing.Structs.Outter.Inner.Equals");

                public static readonly Generated FieldForEncapsulatedValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
                            {
                                private readonly int _value;
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasFieldForEncapsulatedValue,
                    "Monify.Testing.Structs.Outter.Inner._value");

                public static new readonly Generated GetHashCode = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
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
                    "Monify.Testing.Structs.Outter.Inner.GetHashCode");

                public static readonly Generated InequalityOperatorForSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
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
                    "Monify.Testing.Structs.Outter.Inner.Inequality.Self");

                public static readonly Generated InequalityOperatorForValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
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
                    "Monify.Testing.Structs.Outter.Inner.Inequality.Value");

                public static new readonly Generated ToString = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
                        {
                            readonly partial struct Inner
                            {
                                public override string ToString()
                                {
                                    return string.Format("Inner {{ {0} }}", _value);
                                }
                            }
                        }

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.HasToStringOverride,
                    "Monify.Testing.Structs.Outter.Inner.ToString");

                public static readonly Generated UnaryNegationOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.Outter.Inner.UnaryOperators.00");

                public static readonly Generated UnaryPlusOperator = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.None,
                    "Monify.Testing.Structs.Outter.Inner.UnaryOperators.01");

                public static readonly Generated EquatableToSelf = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.IsEquatableToSelf,
                    "Monify.Testing.Structs.Outter.Inner.IEquatable.Self.Equals");

                public static readonly Generated EquatableToValue = new(
                    """
                    namespace Monify.Testing.Structs
                    {
                        using System;
                        using System.Collections.Generic;

                        #nullable disable
                        #pragma warning disable CS8625

                        readonly ref partial struct Outter<T>
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

                        #pragma warning restore CS8625
                        #nullable restore
                    }
                    """,
                    Extensions.IsEquatableToValue,
                    "Monify.Testing.Structs.Outter.Inner.IEquatable.Value.Equals");
            }
        }
    }
}