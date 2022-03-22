using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

[UsesVerify]
public class Tests
{
    [Fact]
    public Task LocationConverter() =>
        Verify(Location.Create(
            "theFile",
            new(1, 2),
            new(
                new(1, 2),
                new(3, 4))));

    [Fact]
    public Task SourceTextConverter() =>
        Verify(SourceText.From("theSource", Encoding.UTF8));

    [Fact]
    public Task LocalizableStringConverter() =>
        Verify(new LocalizableStringImp());

    class LocalizableStringImp :
        LocalizableString
    {
        protected override string GetText(IFormatProvider? formatProvider) =>
            "The Text";

        protected override int GetHash() =>
            throw new NotImplementedException();

        protected override bool AreEqual(object? other) =>
            throw new NotImplementedException();
    }
}