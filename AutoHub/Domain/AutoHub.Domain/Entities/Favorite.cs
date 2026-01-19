using AutoHub.Domain.Base;
using AutoHub.Domain.Exceptions;

namespace AutoHub.Domain.Entities;

public class Favorite(Guid id, Listing listing): Entity<Guid>
{
    public Listing Listing { get;} = listing ?? throw new ArgumentNullValueException(nameof(listing));
    
    public Favorite(Listing listing) : this(Guid.NewGuid(), listing) { }
}