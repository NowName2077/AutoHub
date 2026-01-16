using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Exceptions;

namespace AutoHub.ValueObjects.Validators;

public class MoneyValidator: IValidator<decimal>
{
    public void Validate(decimal value)
    {
        if (value <= 0)
            throw new MoneyAmountNonPositiveException(ExceptionMessages.MONEY_AMOUNT_NON_POSITIVE, nameof(value), value);
        if (!IsValidAmount(value))
            throw new MoneyAmountHasMoreThanTwoDecimalPlacesException(ExceptionMessages.MONEY_AMOUNT_HAS_NOT_MORE_THEN_TWO_DECIMAL_PLACES, nameof(value), value);
    }

    private bool IsValidAmount(decimal value)
    {
        value = value * 100;
        value -= (int)value;
        return value == 0m;
    }
}