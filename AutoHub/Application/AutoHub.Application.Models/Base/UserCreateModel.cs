namespace AutoHub.Application.Models.Base;

public abstract record class UserCreateModel(Guid Id, string Username): IUserCreateModel<Guid>;