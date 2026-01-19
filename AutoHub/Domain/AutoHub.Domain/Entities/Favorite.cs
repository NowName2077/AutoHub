using AutoHub.Domain.Base;
using AutoHub.Domain.Exceptions;

namespace AutoHub.Domain.Entities;

public class Favorite : Entity<Guid>
{
    public Listing Listing { get;}
    
    protected Favorite () {}
    
    public Favorite(Guid id, Listing listing) : base(id)
    {
        Listing = listing ?? throw new ArgumentNullValueException(nameof(listing));
    }

    public Favorite(Listing listing) : this(Guid.NewGuid(), listing) { }
}