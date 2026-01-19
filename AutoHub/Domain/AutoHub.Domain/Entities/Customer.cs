using AutoHub.Domain.Base;
using AutoHub.Domain.Exceptions;
using AutoHub.ValueObjects;
using AutoHub.ValueObjects.Exceptions;

namespace AutoHub.Domain.Entities;

public class Customer(Guid id, Username username) : Entity<Guid>(id)
{
    private readonly ICollection<Favorite> _favorites = new List<Favorite>();
    private readonly ICollection<Listing> _observedListings = new List<Listing>();
    
    public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));
    
    public IReadOnlyCollection<Listing> ActiveObservedListings =>
        _observedListings.Where(lot => lot.IsActive).ToList().AsReadOnly();
    
    public IReadOnlyCollection<Favorite> Favorites => _favorites.ToList().AsReadOnly();
    
    internal bool ChangeUsername(Username newUsername)
    {
        if (Username == newUsername) return false;
        Username = newUsername;
        return true;
    }

    public void AddFavorite(Listing listing)
    {
        if (listing == null) throw new ArgumentNullValueException(nameof(listing));
        if (_favorites.Any(fav => fav.Listing == listing)) return;
        _favorites.Add(new Favorite(listing));
    }

    public void RemoveFavorite(Listing listing)
    {
        if (listing == null) throw new ArgumentNullValueException(nameof(listing));
        var favorite = _favorites.FirstOrDefault(fav => fav.Listing == listing);
        if (favorite != null) _favorites.Remove(favorite);
    }

    public Transaction MakeTransaction(Listing listing, Money money)
    {
        if (listing == null) 
            throw new ArgumentNullValueException(nameof(listing));

        if (!listing.IsActive)
            throw new NotForeseenSituationForThisListingStatusException(listing, listing.Status);

        if (listing.Seller == null)
            throw new NotForeseenSituationForThisListingSellerException();
        
        if (money < listing.Price)
            throw new NotForeseenSituationForThisListingPriceException(money, listing.Price);
        
        listing.Complete(this);

        var transaction = new Transaction(listing, listing.Price, this, listing.Seller, DateTime.UtcNow);
        return transaction;
    }
    
    public void ObserveListing(Listing listing)
    {
        if (listing == null) throw new ArgumentNullException(nameof(listing));
        if (_observedListings.Contains(listing)) return;
        _observedListings.Add(listing);
    }
    
}