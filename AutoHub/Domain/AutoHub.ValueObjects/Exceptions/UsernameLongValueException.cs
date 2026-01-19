namespace AutoHub.ValueObjects.Exceptions;

public class UsernameLongValueException: FormatException
{
    public UsernameLongValueException(string name, int maxLength)
        : base($"Name length '{name}' greater than maximum allowed length {maxLength}")
    {
        Name = name;
        MaxLength = maxLength;
    }

    public string Name { get; }
    public int MaxLength { get; }
}