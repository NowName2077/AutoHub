namespace AutoHub.Application.Models.Base;

public interface IUserCreateModel<out TId> where TId: struct,IEquatable<TId>
{
    public TId Id { get;}
    public string Username { get;}
}