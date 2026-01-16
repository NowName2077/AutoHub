using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Exceptions;

namespace AutoHub.ValueObjects.Validators;

public class EngineVolumeValidator: IValidator<decimal>
{
    public const int MaxValue = 20;
    public const int MinValue = 0;
    
    public void Validate(decimal value)
    {
        if (value > MaxValue)
            throw new ArgumentHighValueException(nameof(value), MaxValue);
        if (value <= MinValue)
            throw new ArgumentLowValueException(nameof(value), MinValue);
    }
}