namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException: ArgumentNullException
{
    public ArgumentNullOrWhiteSpaceException(string paramName, string message)
        : base(paramName, message) { }
}
