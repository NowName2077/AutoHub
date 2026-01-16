namespace AutoHub.ValueObjects.Exceptions;

public class UsernameShortValueException(string name,  int minLength)
    : FormatException($"Name length {name} greater than minimum allowed length {minLength}")
{
    public string Name => name;
    public int MinLength => minLength;
}