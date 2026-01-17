using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions.Base;

namespace AutoHub.Domain.Repositories.Abstractions;

public interface ISellersRepository: IRepository<Seller, Guid>
{
    Task<Seller?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken);
}