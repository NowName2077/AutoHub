using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Exceptions;

namespace AutoHub.ValueObjects.Validators;

public class TitleValidator: IValidator<string>
{
    public static int MaxLength => 50;
    public static int MinLength => 3;
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.TITEL_NOT_NULL_OR_WRITE_SPACES, nameof(value));
        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), MaxLength);
        if (value.Length < MinLength)
            throw new ArgumentShortValueException(nameof(value), MinLength);
    }
}