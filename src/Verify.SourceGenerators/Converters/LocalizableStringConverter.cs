using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

class LocalizableStringConverter :
    WriteOnlyJsonConverter<LocalizableString>
{
    public override void Write(VerifyJsonWriter writer, LocalizableString value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}