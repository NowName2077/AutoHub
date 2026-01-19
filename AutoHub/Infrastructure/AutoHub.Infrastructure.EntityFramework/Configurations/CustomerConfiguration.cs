using AutoHub.Domain.Entities;
using AutoHub.ValueObjects;
using AutoHub.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.Infrastructure.EntityFramework.Configurations;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired();

        builder.Property(c => c.Username)
            .IsRequired()
            .HasConversion(username => username!.Value, value => new Username(value))
            .HasMaxLength(UsernameValidator.MaxLength);
        
        builder.HasMany(c => c.ActiveObservedListings)
            .WithOne()
            .HasForeignKey("CustomerId")
            .HasPrincipalKey(c => c.Id);
        
        builder.HasMany(c => c.Favorites)
            .WithOne()
            .HasForeignKey("CustomerId")
            .HasPrincipalKey(c => c.Id);
    }
}