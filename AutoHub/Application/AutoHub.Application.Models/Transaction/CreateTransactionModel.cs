using AutoHub.Application.Models.Base;

namespace AutoHub.Application.Models.Transaction;

public record class CreateTransactionModel(
    Guid ListingId,
    decimal amount,
    Guid SellerId,
    Guid BuyerId,
    DateTime TransactionDate) : ICreateModel;