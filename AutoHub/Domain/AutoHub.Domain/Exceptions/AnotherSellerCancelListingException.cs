using AutoHub.Domain.Entities;

namespace AutoHub.Domain.Exceptions;

public class AnotherSellerCancelListingException: InvalidOperationException
{
    public AnotherSellerCancelListingException(Listing listing, Seller seller)
        : base($"The seller {seller.Username} can't cancel the {listing.Title} " +
               $"listing owned by the seller {listing.Seller.Username} (lot id = {listing.Id}).")
    {
        Listing = listing;
        Seller = seller;
    }
    public Listing Listing { get; }
    public Seller Seller { get; }
}