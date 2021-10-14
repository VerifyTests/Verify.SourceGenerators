using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        #region Initialize
        VerifySourceGenerators.Enable();
        #endregion
    }
}