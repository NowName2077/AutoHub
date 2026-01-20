using AutoHub.WebHost.Requests.Seller;
using FluentValidation;

namespace AutoHub.WebHost.Validators.Seller;

public class CreateSellerValidator : AbstractValidator<CreateSellerRequest>
{
    public CreateSellerValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}