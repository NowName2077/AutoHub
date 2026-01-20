using AutoHub.Application.Models.Listing;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;
using AutoMapper;

namespace AutoHub.Application.Services;

public class ListingsApplicationService: IListingsApplicationService
{
    private readonly IListingsRepository _listingsRepository;
    private readonly ISellersRepository _sellersRepository;
    private readonly IMapper _mapper;
    

    public ListingsApplicationService(IListingsRepository listingsRepository, ISellersRepository sellersRepository, IMapper mapper)
    {
        _listingsRepository = listingsRepository;
        _sellersRepository = sellersRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ListingModel>> GetListingsAsync(CancellationToken cancellationToken = default)
    {
        var all = await _listingsRepository.GetAllAsync(cancellationToken, true);
        return all.Where(l => l.IsActive).Select(l => _mapper.Map<ListingModel>(l));
    }

    public async Task<IEnumerable<ListingModel>> GetListingsByEndDateAsync(DateTime endDateUtc, CancellationToken cancellationToken = default)
    {
        var all = await _listingsRepository.GetAllByEndDateAsync(endDateUtc, cancellationToken, true);
        return all.Select(l => _mapper.Map<ListingModel>(l));
    }
    public async Task<ListingModel?> GetListingByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _listingsRepository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ListingModel>(entity);
    }
    
    public async Task<ListingModel?> CreateListingAsync(CreateListingModel listingInformation, CancellationToken cancellationToken = default)
    {
        var seller = await _sellersRepository.GetByIdAsync(listingInformation.SellerId, cancellationToken);
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

        var created = await _listingsRepository.AddAsync(listing, cancellationToken);
        return created is null ? null : _mapper.Map<ListingModel>(created);
    }

    public async Task<bool> UpdateListingAsync(ListingModel listingModel, CancellationToken cancellationToken = default)
    {
        var entity = await _listingsRepository.GetByIdAsync(listingModel.Id, cancellationToken);
        if (entity is null) return false;
        
        entity.UpdateTitle(new Title(listingModel.Title));
        entity.UpdatePrice(new Money(listingModel.Price));
        if (Enum.TryParse<FuelType>(listingModel.FuelType, true, out var fuelType))
        { }
        
        EngineVolume? ev = new EngineVolume(listingModel.EngineVolume);
        Horsepower? hp = new Horsepower(listingModel.Horsepower);
        Torque? tq = new Torque(listingModel.Torque);
        Brand? brand = new Brand(listingModel.Brand);
        Color? color = new Color(listingModel.Color);
        
        FuelType? fuel = Enum.TryParse<FuelType>(listingModel.FuelType, true, out var ftmp) ? ftmp : null;
        Aspiration? asp = Enum.TryParse<Aspiration>(listingModel.Aspiration, true, out var astmp) ? astmp : null;
        EngineConfiguration? ec = Enum.TryParse<EngineConfiguration>(listingModel.EngineConfiguration, true, out var ectmp) ? ectmp : null;
        EngineLayout? el = Enum.TryParse<EngineLayout>(listingModel.EngineLayout, true, out var elt) ? elt : null;
        TransmissionType? tt = Enum.TryParse<TransmissionType>(listingModel.TransmissionType, true, out var ttt) ? ttt : null;
        TypeOfDrive? tod = Enum.TryParse<TypeOfDrive>(listingModel.TypeOfDrive, true, out var tott) ? tott : null;
        BodyType? bt = Enum.TryParse<BodyType>(listingModel.BodyType, true, out var btt) ? btt : null;

        entity.UpdateCar(brand, ev, hp, tq, fuel, asp, ec, el, tod, tt, bt, color);
        
        return await _listingsRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task<bool> DeleteListingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _listingsRepository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        return await _listingsRepository.DeleteAsync(entity, cancellationToken);
    }
}