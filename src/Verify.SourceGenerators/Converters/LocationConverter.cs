using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

class LocationConverter :
    WriteOnlyJsonConverter<Location>
{
    public override void Write(VerifyJsonWriter writer, Location value, JsonSerializer serializer)
    {
        writer.WriteValue(value.GetMappedLineSpan().ToString());
    }
}