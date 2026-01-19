using AutoHub.Application.Models.Listing;

namespace AutoHub.Application.Services.Abstractions;

public interface IListingsApplicationService
{
    Task<ListingModel?> GetListingByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ListingModel>> GetListingsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ListingModel>> GetListingsByEndDateAsync(DateTime endDateUtc, CancellationToken cancellationToken);
    Task<ListingModel?> CreateListingAsync(CreateListingModel listingInformation, CancellationToken cancellationToken);
    Task<bool> UpdateListingAsync(ListingModel listing, CancellationToken cancellationToken);
    Task<bool> DeleteListingAsync(Guid id, CancellationToken cancellationToken);
}