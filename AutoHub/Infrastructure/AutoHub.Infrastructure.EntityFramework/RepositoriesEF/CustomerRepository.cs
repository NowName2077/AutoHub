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
    
    public async Task<bool> AddFavoriteAsync(Guid customerId, Listing listing, CancellationToken cancellationToken)
    {
        var customer = await _customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);
        if (customer is null) return false;
        
        var favorite = new Favorite(listing);
        await context.Set<Favorite>().AddAsync(favorite, cancellationToken);
        
        context.Entry(favorite).Property("CustomerId").CurrentValue = customerId;

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}