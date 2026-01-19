using AutoHub.Domain.Entities;
using AutoHub.Domain.Repositories.Abstractions.Base;

namespace AutoHub.Domain.Repositories.Abstractions;
public interface IListingsRepository :IRepository<Listing, Guid>
{
    Task<IEnumerable<Listing>> GetAllByEndDateAsync(
        DateTime endDateUtc,
        CancellationToken cancellationToken,
        bool asNoTracking = false);
}