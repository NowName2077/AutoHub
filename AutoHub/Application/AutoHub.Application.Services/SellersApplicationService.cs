using AutoMapper;
using AutoHub.Application.Models.Seller;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;

namespace AutoHub.Application.Services;

    public class SellersApplicationService: ISellersApplicationService
    {
 private readonly ISellersRepository _repository;
    private readonly IMapper _mapper;

    public SellersApplicationService(ISellersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SellerModel>> GetSellersAsync(CancellationToken cancellationToken = default)
    {
        var all = await _repository.GetAllAsync(cancellationToken, true);
        return all.Select(s => _mapper.Map<SellerModel>(s));
    }

    public async Task<SellerModel?> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var seller = await _repository.GetByIdAsync(id, cancellationToken);
        return seller is null ? null : _mapper.Map<SellerModel>(seller);
    }

    public async Task<SellerModel?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var seller = await _repository.GetSellerByUsernameAsync(username, cancellationToken);
        return seller is null ? null : _mapper.Map<SellerModel>(seller);
    }

    public async Task<SellerModel?> CreateSellerAsync(CreateSellerModel sellerInformation, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(sellerInformation.Id, cancellationToken) is not null)
            return null;

        var seller = new Seller(sellerInformation.Id, new Username(sellerInformation.Username));
        var createdSeller = await _repository.AddAsync(seller, cancellationToken);
        return createdSeller is null ? null : _mapper.Map<SellerModel>(createdSeller);
    }

    public async Task<bool> UpdateSellerAsync(SellerModel seller, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(seller.Id, cancellationToken);
        if (entity is null) return false;

        entity = _mapper.Map<Seller>(seller);
        return await _repository.UpdateAsync(entity, cancellationToken);
    }

    public async Task<bool> DeleteSellerAsync(Guid id, CancellationToken cancellationToken)
    {
        var seller = await _repository.GetByIdAsync(id, cancellationToken);
        return seller is null ? false : await _repository.DeleteAsync(seller, cancellationToken);
    }
    }

