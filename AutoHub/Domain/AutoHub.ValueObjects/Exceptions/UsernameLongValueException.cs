namespace AutoHub.ValueObjects.Exceptions;

public class UsernameLongValueException(string name, int maxLength)
    : FormatException($"Name length {name} greater than maximum allowed length {maxLength}")
{
    public string Name => name;
    public int MaxLength => maxLength;
}