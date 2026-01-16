namespace AutoHub.ValueObjects.Exceptions;

public class ArgumentNullOrWhiteSpaceException(string paramName, string message)
        : ArgumentNullException(paramName, message);
