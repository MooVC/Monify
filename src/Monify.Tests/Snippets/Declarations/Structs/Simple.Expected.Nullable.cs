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
                            return string.Format("Simple {{ {0} }}", _value);
                        }
                    }

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.HasToStringOverride,
                "Monify.Testing.Structs.Simple.ToString");

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
        }
    }
}