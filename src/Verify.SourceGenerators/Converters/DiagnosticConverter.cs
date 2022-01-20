using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

class DiagnosticConverter :
    WriteOnlyJsonConverter<Diagnostic>
{
    public override void Write(VerifyJsonWriter writer, Diagnostic value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Id");
        writer.WriteValue(value.Id);
        writer.WritePropertyName("Title");
        writer.WriteValue(value.Descriptor.Title.ToString());
        writer.WritePropertyName("Severity");
        writer.WriteValue(value.Severity.ToString());
        writer.WritePropertyName("WarningLevel");
        writer.WriteValue(value.WarningLevel);
        writer.WritePropertyName("Location");
        writer.WriteValue(value.Location.GetMappedLineSpan().ToString());

        var description = value.Descriptor.Description.ToString();
        if (!string.IsNullOrWhiteSpace(description))
        {
            writer.WritePropertyName("Description");
            writer.WriteValue(description);
        }

        if (!string.IsNullOrWhiteSpace(value.Descriptor.HelpLinkUri))
        {
            writer.WritePropertyName("HelpLink");
            writer.WriteValue(value.Descriptor.HelpLinkUri);
        }

        writer.WritePropertyName("MessageFormat");
        writer.WriteValue(value.Descriptor.MessageFormat.ToString());

        writer.WritePropertyName("Message");
        writer.WriteValue(value.GetMessage());

        writer.WritePropertyName("Category");
        writer.WriteValue(value.Descriptor.Category);

        if (value.Descriptor.CustomTags.Any())
        {
            writer.WritePropertyName("CustomTags");
            writer.WriteValue(value.Descriptor.CustomTags);
        }

        writer.WriteEndObject();
    }
}