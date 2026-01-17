using AutoHub.Domain.Base;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Seller(Guid id, Username username) : Entity<Guid>(id)
{
    private readonly ICollection<Car> _cars = [];
    private readonly ICollection<Listing> _listings = [];
    
    public Username Username { get; private set; } = username?? throw new ArgumentNullValueException(nameof(username));
    
    internal bool ChangeUsername(Username newUsername) 
    {
        if (Username == newUsername) return false;
        Username = newUsername;
        return true;
    }
    
    public Car CreateCar(Brand brand, EngineVolume engineVolume, Horsepower horsepower, Torque torque, 
        FuelType fuelType, Aspiration aspiration, EngineConfiguration engineConfiguration, EngineLayout engineLayout,
        TypeOfDrive typeOfDrive, TransmissionType transmissionType, BodyType bodyType, Color color)
    {
        var car = new Car(brand, engineVolume, horsepower, torque, fuelType, aspiration, engineConfiguration,
            engineLayout, typeOfDrive, transmissionType, bodyType, color);
        _cars.Add(car);
        return car;
    }
    public Car UpdateCar(
        Car car,
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
        if (car == null) throw new ArgumentNullValueException(nameof(car));

        var owned = _cars.FirstOrDefault(c => c == car)
                    ?? throw new InvalidOperationException("Car does not belong to this seller.");
        
        owned.Update(newBrand, newEngineVolume, newHorsepower, newTorque,
            newFuelType, newAspiration, newEngineConfiguration, newEngineLayout,
            newTypeOfDrive, newTransmissionType, newBodyType, newColor);

        return owned;
    }

    public bool DeleteCar(Car car)
    {
        if (car == null) throw new ArgumentNullValueException(nameof(car));

        var owned = _cars.FirstOrDefault(c => c.Id == car.Id)
                    ?? throw new InvalidOperationException("Car does not belong to this seller.");

        if (_listings != null && _listings.Any(l => l.Car.Id == owned.Id && l.IsActive))
            throw new InvalidOperationException("Cannot delete car that has active listings.");

        return _cars.Remove(owned);
    }
    
    public Listing CreateListing(Title title, Car car, Money price, DateTime startDate)
    {
        if (car == null) throw new ArgumentNullValueException(nameof(car));
        if (!_cars.Contains(car))
            _cars.Add(car);

        var listing = new Listing(title, car, price, startDate, this);
        _listings.Add(listing);
        return listing;
    }

    public Listing UpdateListing(Listing listing,Title? newTitel = null, Money? newPrice = null, Car? newCar = null)
    {
        if (listing == null) throw new ArgumentNullValueException(nameof(listing));
        var owned = _listings.FirstOrDefault(l => l == listing)
                    ?? throw new InvalidOperationException("Listing does not belong to this seller.");

        if (newPrice != null) owned.UpdatePrice(newPrice);
        if (newCar != null) owned.UpdateCar(newCar);
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