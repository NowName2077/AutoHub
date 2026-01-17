using AutoHub.Domain.Base;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Car : Entity<Guid>
{
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
    
    protected Car() { }

    protected Car(Guid id,
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
        Color color) : base(id)
    {
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
        Color = color?? throw new ArgumentNullException(nameof(color));
    }
    public Car(
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
        Color color)
        : this(Guid.NewGuid(),brand, engineVolume, horsepower, torque, fuelType, aspiration, 
            engineConfiguration,engineLayout, typeOfDrive, transmissionType, bodyType, color){ }
    
    public void Update(
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
        if (newBrand!= null) Brand = newBrand;
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