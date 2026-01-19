using AutoHub.Application.Models.Favorite;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Repositories.Abstractions;
using AutoMapper;

namespace AutoHub.Application.Services;

public class FavoritesApplicationService(ICustomersRepository customersRepository,IListingsRepository listingsRepository,IMapper mapper)
    : IFavoritesApplicationService
{

    public async Task<IEnumerable<FavoriteModel>> GetFavoritesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await customersRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null) return Enumerable.Empty<FavoriteModel>();

        return customer.Favorites.Select(f => mapper.Map<FavoriteModel>(f));
    }

    public async Task<bool> AddFavoriteAsync(Guid customerId, Guid listingId, CancellationToken cancellationToken = default)
    {
        var customer = await customersRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null) return false;

        var listing = await listingsRepository.GetByIdAsync(listingId, cancellationToken);
        if (listing is null) return false;
        
        customer.AddFavorite(listing);

        return await customersRepository.UpdateAsync(customer, cancellationToken);
    }

    public async Task<bool> RemoveFavoriteAsync(Guid customerId, Guid listingId, CancellationToken cancellationToken = default)
    {
        var customer = await customersRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null) return false;

        var listing = await listingsRepository.GetByIdAsync(listingId, cancellationToken);
        if (listing is null) return false;

        customer.RemoveFavorite(listing);
        return await customersRepository.UpdateAsync(customer, cancellationToken);
    }
}