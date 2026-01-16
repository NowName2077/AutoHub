using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Exceptions;

namespace AutoHub.ValueObjects.Validators;

public class TorqueValidator: IValidator<int>
{
    public const int MaxValue = 2500;
    public const int MinValue = 10;
    
    public void Validate(int value)
    {
        if (value > MaxValue)
            throw new ArgumentHighValueException(nameof(value), MaxValue);
        if (value < MinValue)
            throw new ArgumentLowValueException(nameof(value), MinValue);
    }
}