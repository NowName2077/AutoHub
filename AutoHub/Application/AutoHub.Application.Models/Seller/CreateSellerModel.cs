using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Seller;


public record class CreateSellerModel(Guid Id, string Username)
    : UserCreateModel(Id, Username);