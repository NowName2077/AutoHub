using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Seller;

public record class SellerModel(Guid Id, string Username) : UserModel(Id, Username) 
{
    public IEnumerable<CarLotModel> ActiveCarLots { get; init; }
}