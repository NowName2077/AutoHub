using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class EngineVolume: ValueObject<decimal>
{
    public EngineVolume(decimal liters) : base(new EngineVolumeValidator(), liters) { }
}