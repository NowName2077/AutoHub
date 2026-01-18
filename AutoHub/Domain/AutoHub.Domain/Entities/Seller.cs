using AutoHub.Domain.Base;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Seller(Guid id, Username username) : Entity<Guid>(id)
{
    private readonly ICollection<Listing> _listings = [];
    
    public Username Username { get; private set; } = username?? throw new ArgumentNullValueException(nameof(username));
    
    internal bool ChangeUsername(Username newUsername) 
    {
        if (Username == newUsername) return false;
        Username = newUsername;
        return true;
    }
    
    public Listing CreateListing(Title title, 
        Brand brand, 
        EngineVolume engineVolume, 
        Horsepower horsepower, 
        Torque torque, 
        FuelType fuelType, 
        Aspiration aspiration, 
        EngineConfiguration engineConfiguration, 
        EngineLayout engineLayout,
        TypeOfDrive typeOfDrive, 
        TransmissionType transmissionType, 
        BodyType bodyType, 
        Color color, 
        Money price, 
        DateTime startDate)
    {
        var listing = new Listing(title, brand, engineVolume, horsepower, torque, fuelType, aspiration, engineConfiguration,
            engineLayout, typeOfDrive, transmissionType, bodyType, color, price, startDate, this);
        _listings.Add(listing);
        return listing;
    }

    public Listing UpdateListing(
        Listing listing,
        Title? newTitle = null,
        Money? newPrice = null,
        Brand? newBrand = null,
        EngineVolume? newEngineVolume = null,
        Horsepower? newHorsepower = null,
        Torque? newTorque = null,
        FuelType? newFuelType = null,
        Aspiration? newAspiration = null,
        EngineConfiguration? newEngineConfiguration = null,
        EngineLayout? newEngineLayout = null,
        TypeOfDrive? newTypeOfDrive = null,
        TransmissionType? newTransmissionType = null,
        BodyType? newBodyType = null,
        Color? newColor = null)
    {
        if (listing == null) throw new ArgumentNullValueException(nameof(listing));
        var owned = _listings.FirstOrDefault(l => l == listing)
                    ?? throw new InvalidOperationException("Listing does not belong to this seller.");

        if (newTitle != null) owned.UpdateTitle(newTitle);
        if (newPrice != null) owned.UpdatePrice(newPrice);
        owned.UpdateCar(newBrand, newEngineVolume, newHorsepower, newTorque, newFuelType, newAspiration,
            newEngineConfiguration, newEngineLayout, newTypeOfDrive, newTransmissionType, newBodyType, newColor);

        return owned;
    }

    public bool DeleteListing(Listing listing)
    {
        if (listing == null) throw new ArgumentNullValueException(nameof(listing));
        var owned = _listings.FirstOrDefault(l => l == listing)
                    ?? throw new InvalidOperationException("Listing does not belong to this seller.");

        if (owned.IsActive)
            throw new InvalidOperationException("Cannot delete active listing. Cancel it first.");

        return _listings.Remove(owned);
    }

    public bool CancelListing(Listing listing)
    {
        if (listing == null) throw new ArgumentNullValueException(nameof(listing));
        var owned = _listings.FirstOrDefault(l => l == listing)
                    ?? throw new ListingDoesNotBelongToSellerException(this, listing);

        return owned.SetCancel(this);
    }
}