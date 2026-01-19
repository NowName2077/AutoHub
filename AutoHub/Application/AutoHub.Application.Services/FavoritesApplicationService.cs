using AutoHub.Application.Models.Favorite;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Repositories.Abstractions;
using AutoMapper;

namespace AutoHub.Application.Services;

public class FavoritesApplicationService: IFavoritesApplicationService
{

    private readonly ICustomersRepository _customersRepository;
    private readonly IListingsRepository _listingsRepository;
    private readonly IMapper _mapper;

    public FavoritesApplicationService(ICustomersRepository customersRepository, IListingsRepository listingsRepository, IMapper mapper)
    {
        _customersRepository = customersRepository;
        _listingsRepository = listingsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FavoriteModel>> GetFavoritesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var customer = await _customersRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null) return Enumerable.Empty<FavoriteModel>();

        return customer.Favorites.Select(f => _mapper.Map<FavoriteModel>(f));
    }

    public async Task<bool> AddFavoriteAsync(Guid customerId, Guid listingId, CancellationToken cancellationToken = default)
    {
        var customer = await _customersRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null) return false;

        var listing = await _listingsRepository.GetByIdAsync(listingId, cancellationToken);
        if (listing is null) return false;

        customer.AddFavorite(listing);
        return await _customersRepository.UpdateAsync(customer, cancellationToken);
    }

    public async Task<bool> RemoveFavoriteAsync(Guid customerId, Guid listingId, CancellationToken cancellationToken = default)
    {
        var customer = await _customersRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null) return false;

        var listing = await _listingsRepository.GetByIdAsync(listingId, cancellationToken);
        if (listing is null) return false;

        customer.RemoveFavorite(listing);
        return await _customersRepository.UpdateAsync(customer, cancellationToken);
    }
}