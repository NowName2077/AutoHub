using AutoHub.Application.Models.Listing;

namespace AutoHub.WebHost.Responses.Seller;

public record class SellerDetailedResponse(Guid Id, string Username, IEnumerable<ListingModel> Listings);