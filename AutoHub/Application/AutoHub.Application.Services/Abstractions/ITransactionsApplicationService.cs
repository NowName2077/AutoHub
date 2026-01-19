using AutoHub.Application.Models.Transaction;

namespace AutoHub.Application.Services.Abstractions;

public interface ITransactionsApplicationService
{
    Task<TransactionModel?> GetTransactionByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TransactionModel>> GetTransactionsBySellerAsync(Guid sellerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TransactionModel>> GetTransactionsByBuyerAsync(Guid buyerId, CancellationToken cancellationToken = default);
    Task<TransactionModel?> CreateTransactionAsync(CreateTransactionModel createModel, CancellationToken cancellationToken = default);
}