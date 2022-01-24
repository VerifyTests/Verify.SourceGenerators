using Microsoft.CodeAnalysis;

class DiagnosticConverter :
    WriteOnlyJsonConverter<Diagnostic>
{
    public override void Write(VerifyJsonWriter writer, Diagnostic value)
    {
        writer.WriteStartObject();
        writer.WriteProperty(value, value.Id, "Id");
        var descriptor = value.Descriptor;
        writer.WriteProperty(value, descriptor.Title.ToString(), "Title");
        writer.WriteProperty(value, value.Severity.ToString(), "Severity");
        writer.WriteProperty(value, value.WarningLevel, "WarningLevel");
        writer.WriteProperty(value, value.Location.GetMappedLineSpan().ToString(), "Location");
        writer.WriteProperty(value, descriptor.Description.ToString(), "Description");
        writer.WriteProperty(value, descriptor.HelpLinkUri, "HelpLink");
        writer.WriteProperty(value, descriptor.MessageFormat.ToString(), "MessageFormat");
        writer.WriteProperty(value, value.GetMessage(), "Message");
        writer.WriteProperty(value, descriptor.Category, "Category");
        writer.WriteProperty(value, descriptor.CustomTags, "CustomTags");
        writer.WriteEndObject();
    }
}