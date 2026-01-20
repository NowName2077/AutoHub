using AutoHub.WebHost.Requests.Listing;
using FluentValidation;

namespace AutoHub.WebHost.Validators.Listing;

public class CreateListingValidator : AbstractValidator<CreateListingRequest>
{
    public CreateListingValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Brand).NotNull().NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.EngineVolume).GreaterThan(0).LessThanOrEqualTo(20);
        RuleFor(x => x.Horsepower).GreaterThan(10).LessThanOrEqualTo(2000);
        RuleFor(x => x.Torque).GreaterThan(10).LessThanOrEqualTo(2500);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.StartDate).LessThanOrEqualTo(DateTime.UtcNow.AddYears(1)); // пример ограничения
        RuleFor(x => x.SellerId).NotEmpty();
    }
}