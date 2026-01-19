using AutoHub.Application.Models.Transaction;
using AutoHub.Application.Services.Abstractions;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.ValueObjects;
using AutoMapper;

namespace AutoHub.Application.Services;

public class TransactionApplicationService(ITransactionsRepository transactionsRepository,IListingsRepository listingsRepository,
    ICustomersRepository customersRepository,ISellersRepository sellersRepository, IMapper mapper): ITransactionsApplicationService
{

    public async Task<TransactionModel?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var t = await transactionsRepository.GetByIdAsync(id, cancellationToken);
        return t is null ? null : mapper.Map<TransactionModel>(t);
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsBySellerAsync(Guid sellerId, CancellationToken cancellationToken = default)
    {
        var all = await transactionsRepository.GetAllBySellerIdAsync(sellerId, cancellationToken, true);
        return all.Select(t => mapper.Map<TransactionModel>(t));
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByBuyerAsync(Guid buyerId, CancellationToken cancellationToken = default)
    {
        var all = await transactionsRepository.GetAllByBuyerIdAsync(buyerId, cancellationToken, true);
        return all.Select(t => mapper.Map<TransactionModel>(t));
    }

    public async Task<TransactionModel?> CreateTransactionAsync(CreateTransactionModel createModel, CancellationToken cancellationToken = default)
    {
        var listing = await listingsRepository.GetByIdAsync(createModel.ListingId, cancellationToken);
        if (listing is null) return null;
        if (!listing.IsActive) return null;

        var buyer = await customersRepository.GetByIdAsync(createModel.BuyerId, cancellationToken);
        if (buyer is null) return null;
        
        if (listing.Seller is null) return null;
        if (listing.Seller.Id != createModel.SellerId) return null;

        var money = new Money(createModel.amount);
        
        Transaction transaction;
        try
        {
            transaction = buyer.MakeTransaction(listing, money);
        }
        catch (Exception)
        {
            return null;
        }
        
        var listingUpdated = await listingsRepository.UpdateAsync(listing, cancellationToken);
        if (!listingUpdated) return null;

        var addedTx = await transactionsRepository.AddAsync(transaction, cancellationToken);
        return addedTx is null ? null : mapper.Map<TransactionModel>(addedTx);
    }
}