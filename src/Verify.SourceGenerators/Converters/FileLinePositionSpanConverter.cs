using Microsoft.CodeAnalysis;

class FileLinePositionSpanConverter :
    WriteOnlyJsonConverter<FileLinePositionSpan>
{
    public override void Write(VerifyJsonWriter writer, FileLinePositionSpan value) =>
        writer.WriteValue(value.ToString());
}