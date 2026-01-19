using AutoHub.Domain.Entities;

namespace AutoHub.Domain.Exceptions;

public class CancelNotActiveListingException: InvalidOperationException
{
    public CancelNotActiveListingException(Listing listing)
        : base($"Can't cancel an inactive listing {listing.Title} (id = {listing.Id}).")
    {
        Listing = listing;
    }
    public Listing Listing { get; }
}