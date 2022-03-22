using Microsoft.CodeAnalysis;

class LocalizableStringConverter :
    WriteOnlyJsonConverter<LocalizableString>
{
    public override void Write(VerifyJsonWriter writer, LocalizableString value) =>
        writer.WriteValue(value.ToString());
}