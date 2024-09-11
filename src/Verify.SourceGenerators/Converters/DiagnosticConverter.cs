class DiagnosticConverter :
    WriteOnlyJsonConverter<Diagnostic>
{
    public override void Write(VerifyJsonWriter writer, Diagnostic value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.Id, "Id");
        var descriptor = value.Descriptor;
        writer.WriteMember(value, descriptor.Title, "Title");
        writer.WriteMember(value, value.Severity.ToString(), "Severity");
        writer.WriteMember(value, value.WarningLevel, "WarningLevel");
        writer.WriteMember(value, value.Location.GetMappedLineSpan().ToString(), "Location");
        var description = descriptor.Description.ToString(CultureInfo.InvariantCulture);
        if (!string.IsNullOrWhiteSpace(description))
        {
            writer.WriteMember(value, description, "Description");
        }

        var help = descriptor.HelpLinkUri;
        if (!string.IsNullOrWhiteSpace(help))
        {
            writer.WriteMember(value, help, "HelpLink");
        }

        writer.WriteMember(value, descriptor.MessageFormat, "MessageFormat");
        writer.WriteMember(value, value.GetMessage(CultureInfo.InvariantCulture), "Message");
        writer.WriteMember(value, descriptor.Category, "Category");
        writer.WriteMember(value, descriptor.CustomTags, "CustomTags");
        writer.WriteEndObject();
    }
}