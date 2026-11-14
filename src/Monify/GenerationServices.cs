namespace Monify
{
    using System;
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Visitors;
    using BinaryOperatorNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.BinaryOperators.BinaryOperator;
    using ConversionNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Conversions.Conversion;
    using EncapsulatedNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated;
    using InterfaceNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Interfaces.String;
    using MethodNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Methods.PassthroughMethod;
    using PropertyNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Properties.PassthroughProperty;
    using UnaryOperatorNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.UnaryOperators.UnaryOperator;

    /// <summary>
    /// Provides the source generation visitors used by the model navigator.
    /// </summary>
    internal sealed class GenerationServices
        : IServiceProvider
    {
        private static readonly IReadOnlyDictionary<Type, object> _visitors = CreateVisitors();

        /// <inheritdoc/>
        public object GetService(Type serviceType)
        {
            _ = _visitors.TryGetValue(serviceType, out object visitors);

            return visitors;
        }

        private static IReadOnlyDictionary<Type, object> CreateVisitors()
        {
            var visitors = new Dictionary<Type, object>();

            Register<Subject.Graph>(
                visitors,
                new GenerateDebuggerDisplayWhenSubjectVisited(),
                new GenerateEqualityOperatorsWhenSubjectVisited(),
                new GenerateEqualsOverrideWhenSubjectVisited(),
                new GenerateEquatableMembersWhenSubjectVisited(),
                new GenerateGetHashCodeOverrideWhenSubjectVisited(),
                new GenerateInequalityOperatorsWhenSubjectVisited(),
                new GenerateToStringOverrideWhenSubjectVisited());
            Register<EncapsulatedNode>(
                visitors,
                new GenerateConstructorsWhenEncapsulatedVisited(),
                new GenerateConversionsFromValueWhenEncapsulatedVisited(),
                new GenerateConversionsToValueWhenEncapsulatedVisited(),
                new GenerateEqualityOperatorsWhenEncapsulatedVisited(),
                new GenerateEquatableMembersWhenEncapsulatedVisited(),
                new GenerateInequalityOperatorsWhenEncapsulatedVisited(),
                new GenerateValueFieldWhenEncapsulatedVisited());

            Register<BinaryOperatorNode>(visitors, new GenerateBinaryOperatorWhenBinaryOperatorVisited());
            Register<ConversionNode>(visitors, new GenerateConversionOperatorWhenConversionVisited());
            Register<InterfaceNode>(visitors, new GenerateInterfaceWhenInterfaceVisited());
            Register<MethodNode>(visitors, new GeneratePassthroughMethodWhenPassthroughMethodVisited());
            Register<PropertyNode>(visitors, new GeneratePassthroughPropertyWhenPassthroughPropertyVisited());
            Register<Subject.Graph.Serialization>(visitors, new GenerateJsonConverterWhenSerializationVisited());
            Register<UnaryOperatorNode>(visitors, new GenerateUnaryOperatorWhenUnaryOperatorVisited());

            return visitors;
        }

        private static void Register<TNode>(Dictionary<Type, object> services, params ISubjectVisitor<TNode, Source>[] visitors)
            where TNode : class, IGraph<Subject>
        {
            services.Add(typeof(IEnumerable<ISubjectVisitor<TNode, Source>>), visitors);
        }
    }
}