using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
