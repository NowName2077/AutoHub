namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentLongValueException(string value, int maxLength)
    : FormatException($"Title length {value} shorter than minimum allowed length {maxLength}")
{
    public string Value => value;
    public int MaxLength => maxLength;
}