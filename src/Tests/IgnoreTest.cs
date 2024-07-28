public class IgnoreTest
{
    #region IgnoreFile

    [Fact]
    public Task IgnoreFile()
    {
        var driver = GeneratorDriver();

        return Verify(driver)
            .IgnoreGeneratedResult(
                _ => _.HintName.Contains("helper") ||
                     _.SourceText
                         .ToString()
                         .Contains("static void SayHello()"));
    }

    #endregion

    [Fact]
    public Task SettingsIgnoreFile()
    {
        var driver = GeneratorDriver();
        var settings = new VerifySettings();
        settings.IgnoreGeneratedResult(
            _ => _.HintName.Contains("helper") ||
                 _.SourceText
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