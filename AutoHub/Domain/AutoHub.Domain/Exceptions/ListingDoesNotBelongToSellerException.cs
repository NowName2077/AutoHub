using AutoHub.Domain.Entities;

namespace AutoHub.Domain.Exceptions;

public class ListingDoesNotBelongToSellerException : InvalidOperationException
{
    public ListingDoesNotBelongToSellerException(Seller seller, Listing listing)
        : base($"The listing {listing.Title} is not in the seller's listing sequence (seller {seller.Username}, lot id = {listing.Id}).")
    {
        Seller = seller;
        Listing = listing;
    }
    
    public Seller Seller { get; }
    public Listing Listing { get; }
}
