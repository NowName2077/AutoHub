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
    public class TransactionsRepository(ApplicationDbContext context): EFRepository<Transaction, Guid>(context), ITransactionsRepository
    {
        private readonly DbSet<Transaction> _transactions = context.Set<Transaction>();



        public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<Transaction> q = _transactions
                .Include(t => t.Listing)
                    .ThenInclude(l => l.Seller)
                .Include(t => t.Buyer)
                .Include(t => t.Seller);

            if (asNoTracking) q = q.AsNoTracking();
            return await q.ToListAsync(cancellationToken);
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _transactions
                .Include(t => t.Listing)
                    .ThenInclude(l => l.Seller)
                .Include(t => t.Buyer)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<Transaction?> AddAsync(Transaction entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            await _transactions.AddAsync(entity, cancellationToken);
            return await context.SaveChangesAsync(cancellationToken) > 0 ? entity : null;
        }

        public async Task<bool> UpdateAsync(Transaction entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _transactions.Update(entity);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Transaction entity, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            _transactions.Remove(entity);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var e = await _transactions.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (e == null) return false;
            _transactions.Remove(e);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<Transaction>> GetAllBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<Transaction> q = _transactions
                .Include(t => t.Listing)
                .Include(t => t.Buyer)
                .Include(t => t.Seller)
                .Where(t => t.Seller.Id == sellerId);

            if (asNoTracking) q = q.AsNoTracking();
            return await q.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Transaction>> GetAllByBuyerIdAsync(Guid buyerId, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<Transaction> q = _transactions
                .Include(t => t.Listing)
                .Include(t => t.Buyer)
                .Include(t => t.Seller)
                .Where(t => t.Buyer.Id == buyerId);

            if (asNoTracking) q = q.AsNoTracking();
            return await q.ToListAsync(cancellationToken);
        }
    }
}