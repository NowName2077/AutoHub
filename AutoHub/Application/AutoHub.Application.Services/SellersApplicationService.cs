using AutoMapper;
using AutoHub.Application.Models.Seller;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;

namespace AutoHub.Application.Services;

    public class SellersApplicationService(ISellersRepository repository, IMapper mapper) : ISellersApplicationService
    {
        public async Task<IEnumerable<SellerModel>> GetSellersAsync(CancellationToken cancellationToken = default)
        {
            var all = await repository.GetAllAsync(cancellationToken, true);
            return all.Select(mapper.Map<SellerModel>);
        }

        public async Task<SellerModel?> GetSellerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var seller = await repository.GetByIdAsync(id, cancellationToken);
            return seller is null ? null : mapper.Map<SellerModel>(seller);
        }

        public async Task<SellerModel?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var seller = await repository.GetSellerByUsernameAsync(username, cancellationToken);
            return seller is null ? null : mapper.Map<SellerModel>(seller);
        }
        public async Task<SellerModel?> CreateSellerAsync(CreateSellerModel sellerInformation, CancellationToken cancellationToken = default)
        {
            if (await repository.GetByIdAsync(sellerInformation.Id, cancellationToken) is not null)
                return null;

            Seller seller = new(sellerInformation.Id, new Username(sellerInformation.Username));
            var createdSeller = await repository.AddAsync(seller, cancellationToken);
            return createdSeller is null ? null : mapper.Map<SellerModel>(createdSeller);
        }

        public async Task<bool> UpdateSellerAsync(SellerModel seller, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetByIdAsync(seller.Id, cancellationToken);
            if (entity is null)
                return false;

            entity = mapper.Map<Seller>(seller);
            return await repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<bool> DeleteSellerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var seller = await repository.GetByIdAsync(id, cancellationToken);
            return seller is null ? false : await repository.DeleteAsync(seller, cancellationToken);
        }
    }

