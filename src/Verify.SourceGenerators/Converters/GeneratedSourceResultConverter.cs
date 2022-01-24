using Microsoft.CodeAnalysis;

class GeneratedSourceResultConverter :
    WriteOnlyJsonConverter<GeneratedSourceResult>
{
    public override void Write(VerifyJsonWriter writer, GeneratedSourceResult value)
    {
        writer.WriteStartObject();
        writer.WriteProperty(value, value.HintName, "HintName");
        writer.WriteProperty(value, value.ToString(), "Source");
        writer.WriteEndObject();
    }
}