using AutoHub.ValueObjects.Base;
using AutoHub.ValueObjects.Validators;

namespace AutoHub.ValueObjects;

public class Title: ValueObject<string>
{
    public Title(string title): base(new TitleValidator(), title){ }
}