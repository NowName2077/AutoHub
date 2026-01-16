namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentShortValueException(string value, int minLength)
    : FormatException($"Title length {value} shorter than minimum allowed length {minLength}")
{
    public string Value => value;
    public int MinLength => minLength;
}