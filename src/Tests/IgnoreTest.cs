public class IgnoreTest
{
    #region IgnoreFile

    [Fact]
    public Task IgnoreFile()
    {
        var driver = GeneratorDriver();

        return Verify(driver)
            .IgnoreGeneratedResultInstance(
                _ => _.HintName.Contains("helper"))
            .IgnoreGeneratedResultInstance(
                _ => _
                    .SourceText
                    .ToString()
                    .Contains("static void SayHello()"));
    }

    #endregion

    [Fact]
    public Task SettingsIgnoreFile()
    {
        var driver = GeneratorDriver();
        var settings = new VerifySettings();
        settings.IgnoreGeneratedResultInstance(
            _ => _.HintName.Contains("helper"));
        settings.IgnoreGeneratedResultInstance(
            _ => _
                .SourceText
                .ToString()
                .Contains("static void SayHello()"));

        return Verify(driver, settings);
    }

    static GeneratorDriver GeneratorDriver()
    {
        var compilation = CSharpCompilation.Create("name");
        var generator = new HelloWorldGenerator();

        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}