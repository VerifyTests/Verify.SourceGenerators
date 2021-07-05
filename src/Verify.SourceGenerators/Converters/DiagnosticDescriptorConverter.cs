using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using VerifyTests;

class DiagnosticDescriptorConverter :
    WriteOnlyJsonConverter<DiagnosticDescriptor>
{
    public override void WriteJson(
        JsonWriter writer,
        DiagnosticDescriptor value,
        JsonSerializer serializer,
        IReadOnlyDictionary<string, object> context)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Id");
        writer.WriteValue(value.Id);
        writer.WritePropertyName("Title");
        writer.WriteValue(value.Title.ToString());

        var description = value.Description.ToString();
        if (!string.IsNullOrWhiteSpace(description))
        {
            writer.WritePropertyName("Description");
            writer.WriteValue(description);
        }

        if (!string.IsNullOrWhiteSpace(value.HelpLinkUri))
        {
            writer.WritePropertyName("HelpLin");
            writer.WriteValue(value.HelpLinkUri);
        }

        writer.WritePropertyName("MessageFormat");
        writer.WriteValue(value.MessageFormat.ToString());

        writer.WritePropertyName("Category");
        writer.WriteValue(value.Category);

        writer.WritePropertyName("DefaultSeverity");
        writer.WriteValue(value.DefaultSeverity.ToString());

        writer.WritePropertyName("IsEnabledByDefault");
        writer.WriteValue(value.IsEnabledByDefault);

        if (value.CustomTags.Any())
        {
            writer.WritePropertyName("CustomTags");
            writer.WriteValue(value.CustomTags);
        }

        writer.WriteEndObject();
    }
}