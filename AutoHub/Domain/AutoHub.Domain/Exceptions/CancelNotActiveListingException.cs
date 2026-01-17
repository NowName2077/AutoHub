using AutoHub.Domain.Entities;

namespace AutoHub.Domain.Exceptions;

public class CancelNotActiveListingException(Listing listing)
    : InvalidOperationException($"Can't cancel an inactive listing {listing.Title} (id = {listing.Id}).")
{
    public Listing Listing => listing;
}