class LocationConverter :
    WriteOnlyJsonConverter<Location>
{
    public override void Write(VerifyJsonWriter writer, Location value)
    {
        var lineSpan = value.GetMappedLineSpan();

        WriteSourceTree(writer, value, lineSpan);

        writer.Serialize(lineSpan);
    }

    static void WriteSourceTree(VerifyJsonWriter writer, Location value, FileLinePositionSpan lineSpan)
    {
        // Pretty-print the error with the source code snippet.
        if (value.SourceTree is not { } source)
        {
            return;
        }

        var comment = new StringBuilder();
        comment.AppendLine();
        var lines = source.GetText().Lines;
        var startLine = Math.Max(lineSpan.StartLinePosition.Line - 1, 0);
        var endLine = Math.Min(lineSpan.EndLinePosition.Line + 1, lines.Count - 1);
        for (var index = startLine; index <= endLine; index++)
        {
            var line = lines[index];
            // print the source line
            comment.AppendLine(line.ToString());
            // print squiggly line highlighting the location
            if (line.Span.Intersection(value.SourceSpan) is not { } intersection)
            {
                continue;
            }

            comment.Append(' ', intersection.Start - line.Start);
            comment.Append('^', intersection.Length);
            comment.AppendLine();
        }

        writer.WriteComment(comment.ToString());
        writer.WriteWhitespace("\n");
    }
}