public class SyntaxLocationTests
{
    [Fact]
    public Task ConsistentLocation()
    {
        var sourceText =
            """
            class Foo {
            void Bar() { }
            }
            """;

        var source = SourceText.From(sourceText);
        var syntaxTree = SyntaxFactory.ParseSyntaxTree(source, path: "theFile.cs");

        var methodDeclarationSyntax = syntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Single();
        return Verify(methodDeclarationSyntax.GetLocation());
    }
}
