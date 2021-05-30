using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using VerifyTests;

class GeneratedSourceResultConverter :
    WriteOnlyJsonConverter<GeneratedSourceResult>
{
    public override void WriteJson(
        JsonWriter writer,
        GeneratedSourceResult value,
        JsonSerializer serializer,
        IReadOnlyDictionary<string, object> context)
    {
        writer.WritePropertyName("HintName");
        writer.WriteValue(value.HintName);
        writer.WritePropertyName("Source");
        writer.WriteValue(value.ToString());
    }
}