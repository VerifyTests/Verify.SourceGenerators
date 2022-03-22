using Microsoft.CodeAnalysis;

class LocationConverter :
    WriteOnlyJsonConverter<Location>
{
    public override void Write(VerifyJsonWriter writer, Location value) =>
        writer.WriteValue(value.GetMappedLineSpan().ToString());
}