using Microsoft.CodeAnalysis;

class GeneratedSourceResultConverter :
    WriteOnlyJsonConverter<GeneratedSourceResult>
{
    public override void Write(VerifyJsonWriter writer, GeneratedSourceResult value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.HintName, "HintName");
        writer.WriteMember(value, value.ToString(), "Source");
        writer.WriteEndObject();
    }
}