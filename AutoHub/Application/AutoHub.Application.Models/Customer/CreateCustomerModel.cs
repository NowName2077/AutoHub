using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Customer;

public record class CreateCustomerModel(Guid Id, string Username)
    : UserCreateModel(Id, Username);