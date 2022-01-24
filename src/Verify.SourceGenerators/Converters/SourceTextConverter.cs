using Microsoft.CodeAnalysis.Text;

class SourceTextConverter :
    WriteOnlyJsonConverter<SourceText>
{
    public override void Write(VerifyJsonWriter writer, SourceText value)
    {
        writer.WriteValue(value.ToString());
    }
}