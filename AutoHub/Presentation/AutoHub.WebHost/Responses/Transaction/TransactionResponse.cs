namespace AutoHub.WebHost.Responses.Transaction;

public record class TransactionResponse(Guid Id, Guid ListingId, decimal Amount, 
    Guid SellerId, Guid BuyerId, DateTime TransactionDate);