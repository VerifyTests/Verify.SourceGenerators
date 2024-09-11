class FileLinePositionSpanConverter :
    WriteOnlyJsonConverter<FileLinePositionSpan>
{
    public override void Write(VerifyJsonWriter writer, FileLinePositionSpan value)
    {
        if (value.IsValid)
        {
            writer.WriteValue($"{value.Path.Replace('/', '\\')}: {value.Span}");
        }
    }
}