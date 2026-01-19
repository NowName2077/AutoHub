using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;

namespace AutoHub.Domain.Exceptions;

public class NotForeseenSituationForThisListingStatusException: InvalidOperationException
{
    public NotForeseenSituationForThisListingStatusException(Listing listing, LotStatus status)
        : base($"Not foreseen situation of transaction for this listing status {status} (id = {listing.Id})")
    {
        Listing = listing;
        Status = status;
    }

    public Listing Listing { get; }
    public LotStatus Status { get; }
}