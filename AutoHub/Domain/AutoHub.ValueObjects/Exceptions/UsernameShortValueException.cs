namespace AutoHub.ValueObjects.Exceptions;

public class UsernameShortValueException: FormatException
{
    public UsernameShortValueException(string name, int minLength)
        : base($"Name length '{name}' is less than minimum allowed length {minLength}")
    {
        Name = name;
        MinLength = minLength;
    }

    public string Name { get; }
    public int MinLength { get; }
}