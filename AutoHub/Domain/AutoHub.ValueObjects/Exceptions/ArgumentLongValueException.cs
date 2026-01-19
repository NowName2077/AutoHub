namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentLongValueException: FormatException
{
    public ArgumentLongValueException(string value, int maxLength)
        : base($"Value '{value}' length is greater than maximum allowed length {maxLength}")
    {
        Value = value;
        MaxLength = maxLength;
    }

    public string Value { get; }
    public int MaxLength { get; }
}