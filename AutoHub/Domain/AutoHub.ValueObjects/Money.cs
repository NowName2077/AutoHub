using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Money(decimal amountInRub) : ValueObject<decimal>(new MoneyValidator(), Math.Round(amountInRub, 2, MidpointRounding.AwayFromZero))
{
    public static Money operator +(Money m1, Money m2)
        => new(m1.Value + m2.Value);

    public static Money operator -(Money m1, Money m2)
        => new(m1.Value - m2.Value);

    public static bool operator >(Money m1, Money m2)
        => m1.Value > m2.Value;

    public static bool operator <(Money m1, Money m2)
        => m1.Value < m2.Value;

    public static bool operator >=(Money m1, Money m2)
        => m1.Value >= m2.Value;

    public static bool operator <=(Money m1, Money m2)
        => m1.Value <= m2.Value;

}