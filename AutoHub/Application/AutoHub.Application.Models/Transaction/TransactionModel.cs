using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Transaction;

public record class TransactionModel (
    Guid Id,
    Guid ListingId,
    decimal amount,
    Guid SellerId,
    Guid BuyerId,
    DateTime TransactionDate): IModel<Guid>;