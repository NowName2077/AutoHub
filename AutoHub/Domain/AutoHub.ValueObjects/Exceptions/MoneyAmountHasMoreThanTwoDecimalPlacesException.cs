namespace AutoHub.ValueObjects.Exceptions;

public class MoneyAmountHasMoreThanTwoDecimalPlacesException: ArgumentException
{
    public MoneyAmountHasMoreThanTwoDecimalPlacesException(string message, string paramName, decimal value)
        : base(message, paramName)
    {
        Value = value;
    }

    public decimal Value { get; }
}