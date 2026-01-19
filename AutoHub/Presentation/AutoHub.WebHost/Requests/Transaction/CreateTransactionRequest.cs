namespace AutoHub.WebHost.Requests.Transaction;

public record class CreateTransactionRequest(Guid ListingId, decimal Amount, Guid SellerId, Guid BuyerId, DateTime TransactionDate);