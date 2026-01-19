using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions;

namespace AutoHub.Infrastructure.EntityFramework.RepositoriesEF
{
    public class ListingsRepository(ApplicationDbContext context): EFRepository<Listing, Guid>(context), IListingsRepository
    {
        private readonly DbSet<Listing> _listings = context.Set<Listing>();
        
        public async Task<IEnumerable<Listing>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<Listing> q = _listings
                .Include(l => l.Seller)
                .Include(l => l.Buyer);

            if (asNoTracking) q = q.AsNoTracking();
            return await q.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Listing>> GetAllByEndDateAsync(DateTime endDateUtc, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var endDate = DateTime.SpecifyKind(endDateUtc, DateTimeKind.Utc);
            IQueryable<Listing> q = _listings
                .Include(l => l.Seller)
                .Include(l => l.Buyer)
                .Where(l => l.StartDate <= endDate);

            if (asNoTracking) q = q.AsNoTracking();
            return await q.ToListAsync(cancellationToken);
        }

        public override Task<Listing?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => _listings.Include(l => l.Seller)
                .Include(l => l.Buyer).FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        public async Task<Listing?> AddAsync(Listing entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await _listings.AddAsync(entity, cancellationToken);
            return await context.SaveChangesAsync(cancellationToken) > 0 ? entity : null;
        }

        public async Task<bool> UpdateAsync(Listing entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _listings.Update(entity);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Listing entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _listings.Remove(entity);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var e = await _listings.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
            if (e == null) return false;
            _listings.Remove(e);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}