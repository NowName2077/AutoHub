using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Title(string titele) : ValueObject<string>(new TitleValidator(), titele);