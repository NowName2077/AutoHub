namespace AutoHub.ValueObjects.Exceptions;

public class ValidatorNullException (string paramName, string message): ArgumentNullException(paramName, message);