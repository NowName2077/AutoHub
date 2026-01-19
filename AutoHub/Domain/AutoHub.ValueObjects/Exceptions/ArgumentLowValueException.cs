namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentLowValueException: FormatException
{
    public ArgumentLowValueException(string value, int minValue)
        : base($"Value '{value}' is less than the allowed value {minValue}")
    {
        Value = value;
        MinValue = minValue;
    }

    public string Value { get; }
    public int MinValue { get; }
}