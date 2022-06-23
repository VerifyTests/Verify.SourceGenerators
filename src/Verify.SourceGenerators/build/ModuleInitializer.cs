namespace VerifyTests
{
    public static class ModuleInitializer
    {
        [global::System.Runtime.CompilerServices.ModuleInitializer]
        public static void Initialize()
        {
            global::VerifyTests.VerifySourceGenerators.Enable();
        }
    }
}