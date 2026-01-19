namespace AutoHub.ValueObjects.Exceptions;

public class ValidatorNullException : ArgumentNullException
{
    public ValidatorNullException(string paramName, string message)
        : base(paramName, message) { }
}