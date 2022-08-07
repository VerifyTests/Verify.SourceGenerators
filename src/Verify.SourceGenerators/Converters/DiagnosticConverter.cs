using Microsoft.CodeAnalysis;

class DiagnosticConverter :
    WriteOnlyJsonConverter<Diagnostic>
{
    public override void Write(VerifyJsonWriter writer, Diagnostic value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.Id, "Id");
        var descriptor = value.Descriptor;
        writer.WriteMember(value, descriptor.Title.ToString(), "Title");
        writer.WriteMember(value, value.Severity.ToString(), "Severity");
        writer.WriteMember(value, value.WarningLevel, "WarningLevel");
        writer.WriteMember(value, value.Location.GetMappedLineSpan().ToString(), "Location");
        writer.WriteMember(value, descriptor.Description.ToString(), "Description");
        writer.WriteMember(value, descriptor.HelpLinkUri, "HelpLink");
        writer.WriteMember(value, descriptor.MessageFormat.ToString(), "MessageFormat");
        writer.WriteMember(value, value.GetMessage(), "Message");
        writer.WriteMember(value, descriptor.Category, "Category");
        writer.WriteMember(value, descriptor.CustomTags, "CustomTags");
        writer.WriteEndObject();
    }
}