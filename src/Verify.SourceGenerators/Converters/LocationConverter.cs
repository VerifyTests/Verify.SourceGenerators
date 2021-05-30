using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using VerifyTests;

class LocationConverter :
    WriteOnlyJsonConverter<Location>
{
    public override void WriteJson(
        JsonWriter writer,
        Location value,
        JsonSerializer serializer,
        IReadOnlyDictionary<string, object> context)
    {
        writer.WriteValue(value.ToString());
    }
}