using AutoHub.Application.Models.Favorite;

namespace AutoHub.Application.Services.Abstractions;

public interface IFavoritesApplicationService
{
    Task<IEnumerable<FavoriteModel>> GetFavoritesAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task<bool> AddFavoriteAsync(Guid customerId, Guid listingId, CancellationToken cancellationToken = default);
    Task<bool> RemoveFavoriteAsync(Guid customerId, Guid listingId, CancellationToken cancellationToken = default);

}