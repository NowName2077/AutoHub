using AutoHub.Domain.Entities;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Exceptions;

public class NotForeseenSituationForThisListingPriceException: InvalidOperationException
{
    public NotForeseenSituationForThisListingPriceException(Money required, Money provided)
        : base($"Insufficient amount to buy the listing. Required: {required}, Provided: {provided}.")
    {
        Required = required;
        Provided = provided;
    }

    public Money Required { get; }
    public Money Provided { get; }
}