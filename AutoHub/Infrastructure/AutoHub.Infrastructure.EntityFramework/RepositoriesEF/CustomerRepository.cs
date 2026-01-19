using Microsoft.EntityFrameworkCore;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;

namespace AutoHub.Infrastructure.EntityFramework.RepositoriesEF;

public class CustomerRepository(ApplicationDbContext context)
    : EFRepository<Customer, Guid>(context), ICustomersRepository
{
    private readonly DbSet<Customer> _customers = context.Set<Customer>();


    public override Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _customers.Include(c => c.Favorites)
            .ThenInclude(f => f.Listing).Include(c => c.ActiveObservedListings)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public Task<Customer?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken)
        => _customers.Include(c => c.Favorites)
            .ThenInclude(f => f.Listing).Include(c => c.ActiveObservedListings)
            .FirstOrDefaultAsync(c => c.Username.Value == username, cancellationToken);
}