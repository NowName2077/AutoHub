using Microsoft.EntityFrameworkCore;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;
using AutoHub.Infrastructure.EntityFramework;

namespace AutoHub.Infrastructure.EntityFramework.RepositoriesEF;

public class SellerRepository(ApplicationDbContext context) : EFRepository<Seller, Guid>(context), ISellersRepository
{
    private readonly DbSet<Seller> _sellers = context.Set<Seller>();

    public override Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _sellers.Include(s => s.ActiveListings)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public Task<Seller?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken)
        => _sellers.Include(s => s.ActiveListings)
            .FirstOrDefaultAsync(s => s.Username.Value == username, cancellationToken);
}