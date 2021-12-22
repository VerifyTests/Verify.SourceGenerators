using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Tests
{
    [Fact]
    public Task LocationConverter()
    {
        return Verify(Location.Create(
            "theFile",
            new TextSpan(1, 2),
            new LinePositionSpan(
                new LinePosition(1, 2),
                new LinePosition(3, 4))));
    }

    [Fact]
    public Task SourceTextConverter()
    {
        return Verify(SourceText.From("theSource", Encoding.UTF8));
    }

    [Fact]
    public Task LocalizableStringConverter()
    {
        return Verify(new LocalizableStringImp());
    }

    class LocalizableStringImp :
        LocalizableString
    {
        protected override string GetText(IFormatProvider? formatProvider)
        {
            return "The Text";
        }

        protected override int GetHash()
        {
            throw new NotImplementedException();
        }

        protected override bool AreEqual(object? other)
        {
            throw new NotImplementedException();
        }
    }
}