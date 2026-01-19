namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentShortValueException: FormatException
{
    public ArgumentShortValueException(string value, int minLength)
        : base($"Value '{value}' length is shorter than minimum allowed length {minLength}")
    {
        Value = value;
        MinLength = minLength;
    }

    public string Value { get; }
    public int MinLength { get; }
}