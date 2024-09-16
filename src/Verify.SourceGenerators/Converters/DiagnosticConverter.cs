class DiagnosticConverter :
    WriteOnlyJsonConverter<Diagnostic>
{
    public override void Write(VerifyJsonWriter writer, Diagnostic value)
    {
        writer.WriteStartObject();
        if (value.Location != Location.None)
        {
            writer.WriteMember(value, value.Location, "Location");
        }
        writer.WriteMember(value, value.GetMessage(CultureInfo.InvariantCulture), "Message");
        writer.WriteMember(value, value.Severity, "Severity");
        if (value.WarningLevel != 0)
        {
            writer.WriteMember(value, value.WarningLevel, "WarningLevel");
        }
        writer.WriteMember(value, value.Descriptor, "Descriptor");
        writer.WriteEndObject();
    }
}
