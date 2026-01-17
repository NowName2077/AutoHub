using AutoHub.Domain.Entities;

namespace AutoHub.Domain.Exceptions;

public class ListingDoesNotBelongToSellerException(Seller seller, Listing listing)
    : InvalidOperationException($"The listing {listing.Title} is not in the seller's listing sequence (seller {seller.Username}, lot id = {listing.Id}).")
{
    public Seller Seller => seller;
    public Listing Listing => Listing;
}
