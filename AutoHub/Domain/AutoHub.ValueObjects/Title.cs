using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Title(string titel) : ValueObject<string>(new TitleValidator(), titel);