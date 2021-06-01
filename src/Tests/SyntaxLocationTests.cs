using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        return Verifier.Verify(methodDeclarationSyntax.GetLocation());
    }
}
