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
    public Task PathTest()
    {
        var path = Path.Combine(AttributeReader.GetProjectDirectory(), "a/b").Replace('/', '\\');
        return Verify(new
        {
           path
        });
    }

    [Fact]
    public Task LocalizableStringConverter() =>
        Verify(new LocalizableStringImp());

    [Fact]
    public Task FileLinePositionSpanConverter() =>
        Verify(new FileLinePositionSpan("the path", new(1,2), new(2,4)));

    [Fact]
    public Task FileLinePositionSpanConverter_Empty() =>
        Verify(new FileLinePositionSpan());

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