namespace Monify.Snippets.Declarations.Records;

internal static partial class Simple
{
    public static class Expected
    {
        public static readonly Generated ConstructorForEncapsulatedValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
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
            "Monify.Testing.Records.Simple.ctor");

        public static readonly Generated ConversionFromValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
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

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasConversionFrom,
            "Monify.Testing.Records.Simple.ConvertFrom");

        public static readonly Generated ConversionToValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
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
            Extensions.HasConversionTo,
            "Monify.Testing.Records.Simple.ConvertTo");

        public static readonly Generated EquatableForValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple : IEquatable<int>
                {
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasEquatableForValue,
            "Monify.Testing.Records.Simple.IEquatable.Value");

        public static readonly Generated EqualityOperatorForValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
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
            "Monify.Testing.Records.Simple.Equality.Value");

        public static readonly Generated FieldForEncapsulatedValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
                {
                    private readonly int _value;
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasFieldForEncapsulatedValue,
            "Monify.Testing.Records.Simple._value");

        public static readonly Generated InequalityOperatorForValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
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
            "Monify.Testing.Records.Simple.Inequality.Value");

        public static readonly Generated EquatableToValue = new(
            """
            namespace Monify.Testing.Records
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                sealed partial record Simple
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
            Extensions.IsEquatableToValue,
            "Monify.Testing.Records.Simple.IEquatable.Value.Equals");
    }
}