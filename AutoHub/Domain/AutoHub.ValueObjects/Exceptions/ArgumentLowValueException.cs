namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentLowValueException(string value, int minValue)
    : FormatException($"Title length {value} is less than the allowed value {minValue}")
{
    public string Value => value;
    public int MinValue => minValue;
}