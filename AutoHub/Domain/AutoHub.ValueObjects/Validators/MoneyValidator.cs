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
            throw new MoneyAmountHasMoreThanTwoDecimalPlacesException(ExceptionMessages.MONEY_AMOUNT_HAS_NOT_MORE_THAN_TWO_DECIMAL_PLACES, nameof(value), value);
    }

    private bool IsValidAmount(decimal value)
    {
        return decimal.Round(value, 2) == value;
    }
}