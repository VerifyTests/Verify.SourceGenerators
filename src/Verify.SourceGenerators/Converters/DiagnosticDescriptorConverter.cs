using Microsoft.CodeAnalysis;

class DiagnosticDescriptorConverter :
    WriteOnlyJsonConverter<DiagnosticDescriptor>
{
    public override void Write(VerifyJsonWriter writer, DiagnosticDescriptor value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.Id, "Id");
        writer.WriteMember(value, value.Title.ToString(), "Title");
        writer.WriteMember(value, value.Description.ToString(), "Description");
        writer.WriteMember(value, value.HelpLinkUri, "HelpLink");
        writer.WriteMember(value, value.MessageFormat.ToString(), "MessageFormat");
        writer.WriteMember(value, value.Category, "Category");
        writer.WriteMember(value, value.DefaultSeverity, "DefaultSeverity");
        writer.WriteMember(value, value.IsEnabledByDefault, "IsEnabledByDefault");
        writer.WriteMember(value, value.CustomTags, "CustomTags");
        writer.WriteEndObject();
    }
}