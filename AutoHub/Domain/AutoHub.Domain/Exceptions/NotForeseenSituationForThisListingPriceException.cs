using AutoHub.Domain.Entities;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Exceptions;

public class NotForeseenSituationForThisListingPriceException(Money price, Money money)
    :InvalidOperationException($"Insufficient {price - money} to buy the listing.");
