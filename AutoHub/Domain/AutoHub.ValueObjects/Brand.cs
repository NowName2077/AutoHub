using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Brand : ValueObject<string>
{
    public Brand (string titel) : base (new BrandValidator(), titel){ }
}