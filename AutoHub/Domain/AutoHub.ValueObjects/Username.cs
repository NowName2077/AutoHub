using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Username(string name) : ValueObject<string>(new UsernameValidator(), name);