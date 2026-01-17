using AutoHub.Domain.Base;
using AutoHub.Domain.Enums;
using AutoHub.Domain.Exceptions;
using AutoHub.ValueObjects;

namespace AutoHub.Domain.Entities;

public class Listing: Entity<Guid>
{
    public Title Title { get; private set; }
    public Car Car { get; private set; }
    public Money Price { get; private set; }
    
    public DateTime StartDate { get; private set; }

    public LotStatus Status { get; private set; }

    public Seller Seller { get; }
    public Customer? Buyer { get; private set; }
    
    public bool IsActive => Status == LotStatus.Active;
    
    public bool IsCompleted => Status == LotStatus.Completed;
    
    protected  Listing() {}

    protected Listing(Guid id,Title title, Car car, Money price, DateTime startDate, LotStatus status, Seller seller): base(id)
    {
        Title = title?? throw new ArgumentNullException(nameof(title));
        Car = car;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        StartDate = startDate;
        Status = status;
        Seller = seller ?? throw new ArgumentNullException(nameof(seller));
    }
    
    public Listing(Title title, Car car, Money price, DateTime startDate, Seller seller) 
        : this(Guid.NewGuid(), title, car, price, startDate, LotStatus.Active, seller){}
    
    public bool SetCancel(Seller seller)
    {
        if (Seller != seller)
            throw new AnotherSellerCancelListingException(this, seller);


        if (!IsActive)
            throw new CancelNotActiveListingException(this);

        Status = LotStatus.Canceled;
        return true;
    }

    public bool Complete(Customer buyer)
    {
        if (!IsActive)
            throw new CancelNotActiveListingException(this);

        Buyer = buyer ?? throw new ArgumentNullException(nameof(buyer));
        Status = LotStatus.Completed;
        return true;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice;
    }

    public void UpdateCar(Car updatedCar)
    {
        Car = updatedCar;
    }
}