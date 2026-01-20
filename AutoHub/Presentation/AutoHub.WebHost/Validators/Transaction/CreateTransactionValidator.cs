using AutoHub.WebHost.Requests.Transaction;
using FluentValidation;

namespace AutoHub.WebHost.Validators.Transaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.SellerId).NotEmpty();
        RuleFor(x => x.BuyerId).NotEmpty();
        RuleFor(x => x.TransactionDate).LessThanOrEqualTo(DateTime.UtcNow);
    }
}