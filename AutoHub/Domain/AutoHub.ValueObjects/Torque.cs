using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Torque : ValueObject<int>
{
    public Torque(int value) : base(new TorqueValidator(), value) { }
}