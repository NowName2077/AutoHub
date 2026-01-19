using AutoHub.Application.Models.Listing;

namespace AutoHub.WebHost.Responses.Customer;

public record class CustomerDetailedResponse(Guid Id, string Username, IEnumerable<ListingModel> ObservedListings);