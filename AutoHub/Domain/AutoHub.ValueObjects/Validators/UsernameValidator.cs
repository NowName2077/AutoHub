using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Exceptions;

namespace AutoHub.ValueObjects.Validators;

public class UsernameValidator : IValidator<string>
{
    public static int MaxLength => 30;
    public static int MinLength => 3;
    
    public void Validate(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.USERNAME_NOT_NULL_OR_WRITE_SPACES,
                nameof(name));
        if (name.Length > MaxLength)
            throw new UsernameLongValueException(name, MaxLength);
        if (name.Length < MinLength)
            throw new UsernameShortValueException(name, MinLength);
    }
}