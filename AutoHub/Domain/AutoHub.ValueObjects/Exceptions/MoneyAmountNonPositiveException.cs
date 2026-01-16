namespace AutoHub.ValueObjects.Exceptions;

public class MoneyAmountNonPositiveException(string message, string paramName, decimal value) : ArgumentException(message, paramName)
{
    public decimal Value => value;
}