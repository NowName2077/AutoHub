using AutoHub.WebHost.Requests.Customer;
using FluentValidation;

namespace AutoHub.WebHost.Validators.Customer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}