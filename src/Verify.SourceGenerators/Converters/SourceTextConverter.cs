using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;

class SourceTextConverter :
    WriteOnlyJsonConverter<SourceText>
{
    public override void Write(VerifyJsonWriter writer, SourceText value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}