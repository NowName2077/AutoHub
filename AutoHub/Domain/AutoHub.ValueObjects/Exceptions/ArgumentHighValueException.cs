namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentHighValueException (string value, int maxValue)
        : FormatException($"{value} is greater than the allowed value {maxValue}")
{ 
        public string Value => value;
        public int MaxLength => maxValue;
}
