class FileLinePositionSpanConverter :
    WriteOnlyJsonConverter<FileLinePositionSpan>
{
    public override void Write(VerifyJsonWriter writer, FileLinePositionSpan value) =>
        writer.WriteValue(value.IsValid ? $"{value.Path.Replace('/', '\\')}: {value.Span}" : "");
}