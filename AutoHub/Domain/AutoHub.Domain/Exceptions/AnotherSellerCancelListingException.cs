using AutoHub.Domain.Entities;

namespace AutoHub.Domain.Exceptions;

public class AnotherSellerCancelListingException(Listing listing, Seller seller)
    : InvalidOperationException($"The seller {seller.Username} can't cancel the {listing.Title} listing owned by the seller  {listing.Seller.Username} (lot id = {listing.Id}).")
{
    public Listing Listing => listing;
    public Seller Seller => seller;
}