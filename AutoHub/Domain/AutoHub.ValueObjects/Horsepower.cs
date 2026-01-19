using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Horsepower : ValueObject<int>
{
    public Horsepower(int value) : base(new HorsepowerValidator(), value) { }
}