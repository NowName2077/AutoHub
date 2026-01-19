using AutoHub.Domain.Base;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Transaction : Entity<Guid>
{
    public Listing Listing { get; private set; }
    public Money Amount { get; private set; }
    public Customer Buyer { get; private set; }
    public Seller Seller { get; private set; }
    public DateTime TransactionDate { get; private set; }
    protected Transaction() { }

    protected Transaction(Guid id, Listing listing, Money amount, Customer buyer, Seller seller, DateTime transactionDate)
        : base(id)
    {
        Listing = listing;
        Amount = amount;
        Buyer = buyer;
        Seller = seller;
        TransactionDate = transactionDate;
    }
    
    public Transaction(Listing listing, Money amount, Customer buyer, Seller seller, DateTime transactionDate)
        : this(Guid.NewGuid(), listing, amount, buyer, seller, transactionDate){ }
}