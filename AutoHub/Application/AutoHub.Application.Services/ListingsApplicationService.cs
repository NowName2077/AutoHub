using AutoHub.Application.Models.Listing;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;
using AutoMapper;

namespace AutoHub.Application.Services;

public class ListingsApplicationService(IListingsRepository listingsRepository, ISellersRepository sellersRepository, IMapper mapper)
    : IListingsApplicationService
{
    public async Task<IEnumerable<ListingModel>> GetListingsAsync(CancellationToken cancellationToken = default)
    {
        var all = await listingsRepository.GetAllAsync(cancellationToken, true);
        return all.Where(l => l.IsActive).Select(l => mapper.Map<ListingModel>(l));
    }

    public async Task<IEnumerable<ListingModel>> GetListingsByEndDateAsync(DateTime endDateUtc, CancellationToken cancellationToken = default)
    {
        var all = await listingsRepository.GetAllByEndDateAsync(endDateUtc, cancellationToken, true);
        return all.Select(l => mapper.Map<ListingModel>(l));
    }
    public async Task<ListingModel?> GetListingByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await listingsRepository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : mapper.Map<ListingModel>(entity);
    }
    
    public async Task<ListingModel?> CreateListingAsync(CreateListingModel listingInformation, CancellationToken cancellationToken = default)
    {
        var seller = await sellersRepository.GetByIdAsync(listingInformation.SellerId, cancellationToken);
        if (seller is null) return null;
        
        if (!Enum.TryParse<FuelType>(listingInformation.FuelType, true, out var fuelType)) return null;
        if (!Enum.TryParse<Aspiration>(listingInformation.Aspiration, true, out var aspiration)) return null;
        if (!Enum.TryParse<EngineConfiguration>(listingInformation.EngineConfiguration, true, out var engineConfig)) return null;
        if (!Enum.TryParse<EngineLayout>(listingInformation.EngineLayout, true, out var engineLayout)) return null;
        if (!Enum.TryParse<TransmissionType>(listingInformation.TransmissionType, true, out var transmissionType)) return null;
        if (!Enum.TryParse<TypeOfDrive>(listingInformation.TypeOfDrive, true, out var typeOfDrive)) return null;
        if (!Enum.TryParse<BodyType>(listingInformation.BodyType, true, out var bodyType)) return null;

        var listing = new Listing(
            new Title(listingInformation.Title),
            new Brand(listingInformation.Brand),
            new EngineVolume(listingInformation.EngineVolume),
            new Horsepower(listingInformation.Horsepower),
            new Torque(listingInformation.Torque),
            fuelType,
            aspiration,
            engineConfig,
            engineLayout,
            typeOfDrive,
            transmissionType,
            bodyType,
            new Color(listingInformation.Color),
            new Money(listingInformation.Price),
            listingInformation.StartDate,
            seller
        );

        var created = await listingsRepository.AddAsync(listing, cancellationToken);
        return created is null ? null : mapper.Map<ListingModel>(created);
    }

    public async Task<bool> UpdateListingAsync(ListingModel listingModel, CancellationToken cancellationToken = default)
    {
        var entity = await listingsRepository.GetByIdAsync(listingModel.Id, cancellationToken);
        if (entity is null) return false;
        
        var updated = mapper.Map<Listing>(listingModel);
        return await listingsRepository.UpdateAsync(updated, cancellationToken);
    }

    public async Task<bool> DeleteListingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await listingsRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await listingsRepository.DeleteAsync(entity, cancellationToken);
    }
}