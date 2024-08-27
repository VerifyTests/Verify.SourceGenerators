using System.Globalization;
using Microsoft.CodeAnalysis;

class DiagnosticDescriptorConverter :
    WriteOnlyJsonConverter<DiagnosticDescriptor>
{
    public override void Write(VerifyJsonWriter writer, DiagnosticDescriptor value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.Id, "Id");
        writer.WriteMember(value, value.Title, "Title");

        var description = value.Description.ToString(CultureInfo.InvariantCulture);
        if (!string.IsNullOrWhiteSpace(description))
        {
            writer.WriteMember(value, description, "Description");
        }

        var help = value.HelpLinkUri;
        if (!string.IsNullOrWhiteSpace(help))
        {
            writer.WriteMember(value, help, "HelpLink");
        }
        writer.WriteMember(value, value.MessageFormat, "MessageFormat");
        writer.WriteMember(value, value.Category, "Category");
        writer.WriteMember(value, value.DefaultSeverity, "DefaultSeverity");
        writer.WriteMember(value, value.IsEnabledByDefault, "IsEnabledByDefault");
        writer.WriteMember(value, value.CustomTags, "CustomTags");
        writer.WriteEndObject();
    }
}