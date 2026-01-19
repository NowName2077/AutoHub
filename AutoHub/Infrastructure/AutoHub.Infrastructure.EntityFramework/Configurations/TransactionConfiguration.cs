using AutoHub.Domain.Entities;
using AutoHub.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.Infrastructure.EntityFramework.Configurations;

public class TransactionConfiguration: IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).IsRequired();
        
        builder.Property(t => t.Amount).IsRequired()
            .HasConversion(
                amount => amount!.Value, 
                value => new Money(value));
        
        builder.Property(t => t.Listing).IsRequired();
        
        builder.Property(t => t.TransactionDate).IsRequired()
            .HasConversion(
                td => DateTime.SpecifyKind(td, DateTimeKind.Utc),
                td => DateTime.SpecifyKind(td, DateTimeKind.Utc));
        
        builder.HasOne(t => t.Seller)
            .WithMany()
            .HasForeignKey("SellerId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.Buyer)
            .WithMany()
            .HasForeignKey("BuyerId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}