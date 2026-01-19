namespace AutoHub.Domain.Exceptions;

public class NotForeseenSituationForThisListingSellerException : InvalidOperationException
{
    public NotForeseenSituationForThisListingSellerException()
        : base("Listing has no seller."){ }
}