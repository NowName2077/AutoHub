using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Horsepower(int value) : ValueObject<int>(new HorsepowerValidator(), value);