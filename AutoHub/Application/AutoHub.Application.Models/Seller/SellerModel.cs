using AutoHub.Application.Models.Base;
using AutoHub.Application.Models.Listing;

namespace AutoHub.Application.Models.Seller;

public record class SellerModel(Guid Id, string Username) : UserModel(Id, Username) 
{
    public IEnumerable<ListingModel> ActiveListings{ get; init; }
}