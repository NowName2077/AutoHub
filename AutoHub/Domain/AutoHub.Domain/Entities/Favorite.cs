using AutoHub.Domain.Base;

namespace AutoHub.Domain.Entities;

public class Favorite: Entity<Guid>
{
    public Listing Listing { get;}

    protected Favorite() { }

    protected Favorite(Guid id, Listing listing) : base(id)
    {
        Listing = listing;
    }
    
    public Favorite(Listing listing) : this(Guid.NewGuid(), listing) { }
}