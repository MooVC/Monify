namespace Monify.Snippets.Declarations.Records;

internal static partial class Simple
{
    public static partial class Expected
    {
        public static class Nullable
        {
            public static readonly Generated ConstructorForEncapsulatedValue = new(
                """
                namespace Monify.Testing.Records
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial record Simple
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
                "Monify.Testing.Records.Simple.ctor");

            public static readonly Generated ConversionFromValue = new(
                """
                namespace Monify.Testing.Records
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial record Simple
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
                "Monify.Testing.Records.Simple.ConvertTo");

            public static readonly Generated EquatableForValue = new(
                """
                namespace Monify.Testing.Records
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial record Simple : IEquatable<int>
                    {
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial record Simple
                    {
                        private readonly int _value;
                    }

                    #pragma warning restore CS8625
                    #nullable restore
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

                    #nullable disable
                    #pragma warning disable CS8625

                    sealed partial record Simple
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
                "Monify.Testing.Records.Simple.Inequality.Value");

            public static readonly Generated EquatableToValue = new(
                """
                namespace Monify.Testing.Records
                {
                    using System;
                    using System.Collections.Generic;

                    #nullable disable
                    #pragma warning disable CS8625

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

                    #pragma warning restore CS8625
                    #nullable restore
                }
                """,
                Extensions.IsEquatableToValue,
                "Monify.Testing.Records.Simple.IEquatable.Value.Equals");
        }
    }
}