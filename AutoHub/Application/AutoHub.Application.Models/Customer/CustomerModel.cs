using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Customer;

public record class CustomerModel(Guid Id, string Username) : UserModel(Id, Username)
{
    public IEnumerable<CarLotModel> ObservedCarLots { get; init; }
}