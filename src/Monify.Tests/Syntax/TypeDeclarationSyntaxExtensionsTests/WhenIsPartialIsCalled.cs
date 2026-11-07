namespace Monify.Syntax.TypeDeclarationSyntaxExtensionsTests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class WhenIsPartialIsCalled
{
    [Test]
    public void GivenNonPartialTypeDeclarationThenReturnsFalse()
    {
        // Arrange
        TypeDeclarationSyntax syntax = SyntaxFactory.StructDeclaration("Sample");

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenPartialTypeDeclarationThenReturnsTrue()
    {
        // Arrange
        TypeDeclarationSyntax syntax = SyntaxFactory
            .ClassDeclaration("Sample")
            .WithModifiers(SyntaxFactory
                .TokenList(SyntaxFactory
                    .Token(SyntaxKind.PartialKeyword)));

        // Act
        bool result = syntax.IsPartial();

        // Assert
        result.ShouldBeTrue();
    }
}