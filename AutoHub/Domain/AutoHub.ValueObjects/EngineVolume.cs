using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class EngineVolume(decimal liters) : ValueObject<decimal>(new EngineVolumeValidator(), liters);