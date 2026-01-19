namespace AutoHub.ValueObjects.Exceptions;

public class MoneyAmountNonPositiveException: ArgumentException
{
    public MoneyAmountNonPositiveException(string message, string paramName, decimal value)
        : base(message, paramName)
    {
        Value = value;
    }

    public decimal Value { get; }
}