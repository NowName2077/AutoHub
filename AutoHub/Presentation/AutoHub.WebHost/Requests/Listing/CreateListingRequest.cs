namespace AutoHub.WebHost.Requests.Listing;

public record class CreateListingRequest(
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
    Guid SellerId);