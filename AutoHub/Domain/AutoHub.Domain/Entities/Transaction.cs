using AutoHub.Domain.Base;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Transaction : Entity<Guid>
{
    public Listing Listing { get; }
    public Money Amount { get; }
    public Customer Buyer { get; }
    public Seller Seller { get; }
    public DateTime TransactionDate { get; }

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
        : this(Guid.NewGuid(), listing, amount, buyer, seller, transactionDate){}
}