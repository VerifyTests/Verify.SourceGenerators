using Microsoft.CodeAnalysis;

class DiagnosticDescriptorConverter :
    WriteOnlyJsonConverter<DiagnosticDescriptor>
{
    public override void Write(VerifyJsonWriter writer, DiagnosticDescriptor value)
    {
        writer.WriteStartObject();
        writer.WriteProperty(value, value.Id, "Id");
        writer.WriteProperty(value, value.Title.ToString(), "Title");
        writer.WriteProperty(value, value.Description.ToString(), "Description");
        writer.WriteProperty(value, value.HelpLinkUri, "HelpLink");
        writer.WriteProperty(value, value.MessageFormat.ToString(), "MessageFormat");
        writer.WriteProperty(value, value.Category, "Category");
        writer.WriteProperty(value, value.DefaultSeverity, "DefaultSeverity");
        writer.WriteProperty(value, value.IsEnabledByDefault, "IsEnabledByDefault");
        writer.WriteProperty(value, value.CustomTags, "CustomTags");
        writer.WriteEndObject();
    }
}