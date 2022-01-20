using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

class GeneratedSourceResultConverter :
    WriteOnlyJsonConverter<GeneratedSourceResult>
{
    public override void Write(VerifyJsonWriter writer, GeneratedSourceResult value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("HintName");
        writer.WriteValue(value.HintName);
        writer.WritePropertyName("Source");
        writer.WriteValue(value.ToString());
        writer.WriteEndObject();
    }
}