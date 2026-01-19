using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions.Base;

namespace AutoHub.Domain.Repositories.Abstractions;

public interface ITransactionsRepository : IRepository<Transaction, Guid>
{
    // Дополнительные query-методы можно добавить при необходимости
    Task<IEnumerable<Transaction>> GetAllBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken, bool asNoTracking = false);
    Task<IEnumerable<Transaction>> GetAllByBuyerIdAsync(Guid buyerId, CancellationToken cancellationToken, bool asNoTracking = false);
}