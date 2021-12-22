using Microsoft.CodeAnalysis.Text;
using Xunit;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VerifyXunit;

[UsesVerify]
public class SyntaxLocationTests
{
    [Fact]
    public Task ConsistentLocation()
    {
        var sourceText =
@"class Foo {
void Bar() { }
}
";

        var source = SourceText.From(sourceText);
        var syntaxTree = SyntaxFactory.ParseSyntaxTree(source);

        var methodDeclarationSyntax = syntaxTree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().Single();
        return Verify(methodDeclarationSyntax.GetLocation());
    }
}
