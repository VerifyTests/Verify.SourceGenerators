class GeneratedSourceResultConverter :
    WriteOnlyJsonConverter<GeneratedSourceResult>
{
    public override void Write(VerifyJsonWriter writer, GeneratedSourceResult value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.HintName, "HintName");
        writer.WriteMember(value, value.SourceText, "Source");
        writer.WriteEndObject();
    }
}