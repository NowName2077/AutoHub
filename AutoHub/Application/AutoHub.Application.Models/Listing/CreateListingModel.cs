using AutoHub.Application.Models.Base;
using AutoHub.Domain.Enums;
using AutoHub.ValueObjects;

namespace AutoHub.Application.Models.Listing;

public record class CreateListingModel(
    string Title,
    string Brand,
    decimal EngineVolume,
    int Horsepower,
    int Torque,
    string FuelType,
    string Aspiration,
    string EngineConfiguration,
    string EngineLayout,
    string TransmissionType,
    string TypeOfDrive,
    string BodyType,
    string Color,
    decimal Price,
    DateTime StartDate,
    Guid SellerId) : ICreateModel;