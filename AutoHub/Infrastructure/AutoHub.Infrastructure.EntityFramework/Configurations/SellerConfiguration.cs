using AutoHub.Domain.Entities;
using AutoHub.ValueObjects;
using AutoHub.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.Infrastructure.EntityFramework.Configurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).IsRequired();

        builder.Property(s => s.Username)
            .IsRequired()
            .HasConversion(username => username!.Value, value => new Username(value))
            .HasMaxLength(UsernameValidator.MaxLength);

        builder.HasMany(s => s.ActiveListings)
            .WithOne(l => l.Seller)
            .HasForeignKey("SellerId")
            .HasPrincipalKey(u => u.Id);
        
    }
}