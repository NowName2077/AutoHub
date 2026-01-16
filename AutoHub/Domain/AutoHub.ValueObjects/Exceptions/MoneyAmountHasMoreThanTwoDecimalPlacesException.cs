namespace AutoHub.ValueObjects.Exceptions;

public class MoneyAmountHasMoreThanTwoDecimalPlacesException(string message, string paramName, decimal value) : ArgumentException(message, paramName)
{
    public decimal Value => value;
}