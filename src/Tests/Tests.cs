using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Tests
{
    [Fact]
    public Task LocalizableStringConverter()
    {
        return Verifier.Verify(new LocalizableStringImp());
    }

    [Fact]
    public Task SourceTextConverter()
    {
        return Verifier.Verify(SourceText.From("theSource", Encoding.UTF8));
    }

    class LocalizableStringImp : LocalizableString
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