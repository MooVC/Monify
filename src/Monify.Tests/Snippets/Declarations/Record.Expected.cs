namespace Monify.Snippets.Declarations;

internal static partial class Record
{
    public static class Expected
    {
        public static readonly Generated ConstructorForEncapsulatedValue = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial sealed record Record
                {
                    public Record(int value)
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
            "Monify.Records.Testing.Record.ctor");

        public static readonly Generated EquatableForSelf = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record : IEquatable<Record>
                {
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasEquatableForSelf,
            "Monify.Records.Testing.Record.IEquatable.Self");

        public static readonly Generated EquatableForValue = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record : IEquatable<int>
                {
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasEquatableForValue,
            "Monify.Records.Testing.Record.IEquatable.Value");

        public static readonly Generated EqualityOperatorForSelf = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial sealed record Record
                {
                    public static bool operator ==(Record left, Record right)
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
            "Monify.Records.Testing.Record.Equality.Self");

        public static readonly Generated EqualityOperatorForValue = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial sealed record Record
                {
                    public static bool operator ==(Record left, int right)
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
            "Monify.Records.Testing.Record.Equality.Value");

        public static new readonly Generated Equals = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial sealed record Record
                {
                    public override bool Equals(object other)
                    {
                        return Equals(other as Record);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasEqualsOverride,
            "Monify.Records.Testing.Record.Equals");

        public static readonly Generated FieldForEncapsulatedValue = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial sealed record Record
                {
                    private readonly int _value;
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasFieldForEncapsulatedValue,
            "Monify.Records.Testing.Record._value");

        public static new readonly Generated GetHashCode = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif
        
                partial record Record
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
            "Monify.Records.Testing.Record.GetHashCode");

        public static readonly Generated InequalityOperatorForSelf = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record
                {
                    public static bool operator !=(Record left, Record right)
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
            "Monify.Records.Testing.Record.Inequality.Self");

        public static readonly Generated InequalityOperatorForValue = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record
                {
                    public static bool operator !=(Record left, int right)
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
            "Monify.Records.Testing.Record.Inequality.Value");

        public static new readonly Generated ToString = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record
                {
                    public override string ToString()
                    {
                        return string.Format("Record { {0} }", _value);
                    }
                }
        
                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.HasToStringOverride,
            "Monify.Records.Testing.Record.ToString");

        public static readonly Generated EquatableToSelf = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record
                {
                    public bool Equals(Record other)
                    {
                        if (ReferenceEquals(this, other))
                        {
                            return true;
                        }

                        if (ReferenceEquals(other, null))
                        {
                            return false;
                        }

                        return _value.Equals(other._value);
                    }
                }

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable restore
                #endif
            }
            """,
            Extensions.IsEquatableToSelf,
            "Monify.Records.Testing.Record.IEquatable.Self.Equals");

        public static readonly Generated EquatableToValue = new(
            """
            namespace Monify.Records.Testing
            {
                using System;
                using System.Collections.Generic;

                #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                #nullable disable
                #endif

                partial record Record
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
            "Monify.Records.Testing.Record.IEquatable.Value.Equals");
    }
}