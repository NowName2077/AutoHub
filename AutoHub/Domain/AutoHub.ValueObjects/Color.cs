using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class  Color(string color) : ValueObject<string>(new ColorValidator(), color);