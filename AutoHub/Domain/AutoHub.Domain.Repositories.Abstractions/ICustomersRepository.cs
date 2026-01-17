using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions.Base;

namespace AutoHub.Domain.Repositories.Abstractions;

public interface ICustomersRepository : IRepository<Customer, Guid>
{
    Task<Customer?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken);
}