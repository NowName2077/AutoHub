using AutoHub.Domain.Base;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Listing: Entity<Guid>
{
    public Title Title { get; private set; }
    #region Car
    public Brand Brand { get; private set;}
    
    #region Engine

    public EngineVolume EngineVolume { get; private set;}
    public Horsepower Horsepower { get; private set;}
    public Torque Torque { get; private set;}
    public FuelType FuelType { get; private set;}
    public Aspiration Aspiration { get; private set;}
    public EngineConfiguration EngineConfiguration { get; private set;}
    public EngineLayout EngineLayout { get; private set;}

    #endregion //Engine
    
    #region Transmission

    public TransmissionType TransmissionType { get; private set;}
    public TypeOfDrive TypeOfDrive { get; private set;}

    #endregion //Transmission
    
    #region Body

    public BodyType BodyType { get; private set;}
    public Color Color { get; private set;}

    #endregion //Body
    #endregion //Car
    public Money Price { get; private set; }
    
    public DateTime StartDate { get; private set; }

    public LotStatus Status { get; private set; }

    public Seller Seller { get; }
    public Customer? Buyer { get; private set; }
    
    public bool IsActive => Status == LotStatus.Active;
    
    public bool IsCompleted => Status == LotStatus.Completed;
    
    protected  Listing() { }

    protected Listing(
        Guid id,
        Title title, 
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
        DateTime startDate,
        LotStatus status,
        Seller seller): base(id)
    {
        Title = title?? throw new ArgumentNullValueException(nameof(title));
        Brand = brand ?? throw new ArgumentNullValueException(nameof(brand));
        EngineVolume = engineVolume;
        Horsepower = horsepower;
        Torque = torque;
        FuelType = fuelType;
        Aspiration = aspiration;
        EngineConfiguration = engineConfiguration;
        EngineLayout = engineLayout;
        TypeOfDrive = typeOfDrive;
        TransmissionType = transmissionType;
        BodyType = bodyType;
        Color = color?? throw new ArgumentNullValueException(nameof(color));
        Price = price ?? throw new ArgumentNullValueException(nameof(price));
        StartDate = startDate;
        Status = status;
        Seller = seller ?? throw new ArgumentNullValueException(nameof(seller));
    }
    
    public Listing(Title title,
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
        DateTime startDate,
        Seller seller)
        : this(Guid.NewGuid(), title, brand, engineVolume, horsepower, torque, fuelType, aspiration, 
            engineConfiguration, engineLayout, typeOfDrive, transmissionType, bodyType, color, 
            price, startDate, LotStatus.Active, seller){ }
    
    public bool SetCancel(Seller seller)
    {
        if (Seller != seller)
            throw new AnotherSellerCancelListingException(this, seller);


        if (!IsActive)
            throw new CancelNotActiveListingException(this);

        Status = LotStatus.Canceled;
        return true;
    }

    public bool Complete(Customer buyer)
    {
        if (!IsActive)
            throw new CancelNotActiveListingException(this);

        Buyer = buyer ?? throw new ArgumentNullValueException(nameof(buyer));
        Status = LotStatus.Completed;
        return true;
    }
    
    public void UpdateTitle(Title newTitle)
    {
        Title = newTitle ?? throw new ArgumentNullValueException(nameof(newTitle));
    }
    
    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullValueException(nameof(newPrice));
    }

    public void UpdateCar(
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
        if (newBrand != null) Brand = newBrand;
        if (newEngineVolume != null) EngineVolume = newEngineVolume;
        if (newHorsepower != null) Horsepower = newHorsepower;
        if (newTorque != null) Torque = newTorque;
        if (newFuelType != null) FuelType = newFuelType.Value;
        if (newAspiration != null) Aspiration = newAspiration.Value;
        if (newEngineConfiguration != null) EngineConfiguration = newEngineConfiguration.Value;
        if (newEngineLayout != null) EngineLayout = newEngineLayout.Value;
        if (newTypeOfDrive != null) TypeOfDrive = newTypeOfDrive.Value;
        if (newTransmissionType != null) TransmissionType = newTransmissionType.Value;
        if (newBodyType != null) BodyType = newBodyType.Value;
        if (newColor != null) Color = newColor;
    }
}