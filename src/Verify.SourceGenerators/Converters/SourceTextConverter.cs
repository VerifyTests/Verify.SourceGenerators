using System.Collections.Generic;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;
using VerifyTests;

class SourceTextConverter :
    WriteOnlyJsonConverter<SourceText>
{
    public override void WriteJson(
        JsonWriter writer,
        SourceText value,
        JsonSerializer serializer,
        IReadOnlyDictionary<string, object> context)
    {
        writer.WriteValue(value.ToString());
    }
}