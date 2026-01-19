using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Username : ValueObject<string>
{
    public Username(string name) : base(new UsernameValidator(), name) { }
}