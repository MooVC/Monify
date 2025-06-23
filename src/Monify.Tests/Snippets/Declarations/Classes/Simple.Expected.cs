namespace Monify.Snippets.Declarations.Classes;

internal static partial class Simple
{
    public static class Expected
    {
        public static readonly Generated ConstructorForEncapsulatedValue = new(
            """
            namespace Monify.Testing.Classes
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public Simple(int value)
                    {
                        _value = value;
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public static implicit operator int(Simple subject)
                    {
                        if (subject == null)
                        {
                            throw new ArgumentNullException("subject");
                        }

                        return subject._value;
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public static implicit operator Simple(int value)
                    {
                        return new Simple(value);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasConversionFrom,
            "Monify.Testing.Classes.Simple.ConvertTo");

        public static readonly Generated EquatableForSelf = new(
            """
            namespace Monify.Testing.Classes
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple : IEquatable<Simple>
                {
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple : IEquatable<int>
                {
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public override bool Equals(object other)
                    {
                        return Equals(other as Simple);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    private readonly int _value;
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public override int GetHashCode()
                    {
                        return _value.GetHashCode();
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public static bool operator !=(Simple left, Simple right)
                    {
                        return !(left == right);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public static bool operator !=(Simple left, int right)
                    {
                        return !(left == right);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial class Simple
                {
                    public override string ToString()
                    {
                        return string.Format("Simple { {0} }", _value);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasToStringOverride,
            "Monify.Testing.Classes.Simple.ToString");

        public static readonly Generated EquatableToSelf = new(
            """
            namespace Monify.Testing.Classes
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasEquatableForValue,
            "Monify.Testing.Classes.Simple.IEquatable.Value.Equals");
    }
}
