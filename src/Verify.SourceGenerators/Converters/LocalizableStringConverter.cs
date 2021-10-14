using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using VerifyTests;

class LocalizableStringConverter :
    WriteOnlyJsonConverter<LocalizableString>
{
    public override void WriteJson(
        JsonWriter writer,
        LocalizableString value,
        JsonSerializer serializer,
        IReadOnlyDictionary<string, object> context)
    {
        writer.WriteValue(value.ToString());
    }
}