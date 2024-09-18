class LocationConverter :
    WriteOnlyJsonConverter<Location>
{
    const int contextLines = 1;

    public override void Write(VerifyJsonWriter writer, Location value)
    {
        var lineSpan = value.GetMappedLineSpan();

        // Pretty-print the error with the source code snippet.
        if (value.SourceTree is { } source)
        {
            var comment = new StringBuilder().AppendLine();
            var lines = source.GetText().Lines;
            var startLine = Math.Max(lineSpan.StartLinePosition.Line - contextLines, 0);
            var endLine = Math.Min(lineSpan.EndLinePosition.Line + contextLines, lines.Count - 1);
            for (var lineIdx = startLine; lineIdx <= endLine; lineIdx++)
            {
                var line = lines[lineIdx];
                // print the source line
                comment.AppendLine(line.ToString());
                // print squiggly line highlighting the location
                if (line.Span.Intersection(value.SourceSpan) is { } intersection)
                {
                    comment
                        .Append(' ', intersection.Start - line.Start)
                        .Append('^', intersection.Length)
                        .AppendLine();
                }
            }
            writer.WriteComment(comment.ToString());
            writer.WriteWhitespace("\n");
        }

        writer.Serialize(lineSpan);
    }
}
