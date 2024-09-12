using Microsoft.CodeAnalysis;

[Generator]
public class HelloWorldVbGeneratorWithoutLocation :
    HelloWorldVbGenerator
{
    protected override Location? GetLocation() => null;
}