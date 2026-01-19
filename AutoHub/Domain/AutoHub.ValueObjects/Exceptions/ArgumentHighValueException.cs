namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentHighValueException : FormatException
{ 
    public ArgumentHighValueException(string value, int maxValue)
        : base($"{value} is greater than the allowed value {maxValue}")
    {
        Value = value;
        MaxValue = maxValue;
    }

    public string Value { get; }
    public int MaxValue { get; }
}