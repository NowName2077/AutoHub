using AutoHub.Application.Models.Base;
using AutoHub.Application.Models.Listing;

namespace AutoHub.Application.Models.Customer;

public record class CustomerModel(Guid Id, string Username) : UserModel(Id, Username)
{
    public IEnumerable<ListingModel> ObservedListings { get; init; }
}