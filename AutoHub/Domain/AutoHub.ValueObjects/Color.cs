using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class  Color: ValueObject<string>
{
    public Color(string color) : base(new ColorValidator(), color) { }
}