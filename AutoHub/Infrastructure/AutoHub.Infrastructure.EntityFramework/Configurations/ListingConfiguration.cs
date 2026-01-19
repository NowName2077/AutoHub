using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using AutoHub.ValueObjects;
using AutoHub.ValueObjects.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.Infrastructure.EntityFramework.Configurations;

public class ListingConfiguration: IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).IsRequired();
        
        builder.Property(l => l.Title).IsRequired()
            .HasConversion(
                title => title!.Value, 
                value => new Title(value))
            .HasMaxLength(TitleValidator.MaxLength);
        #region Car
        builder.Property(l => l.Brand).IsRequired()
            .HasConversion(
                brand => brand!.Value,
                value => new Brand(value))
            .HasMaxLength(BrandValidator.MaxLength);
        
        #region Engine
        builder.Property(l => l.EngineVolume).IsRequired()
            .HasConversion(
                ev => ev!.Value, 
                value => new EngineVolume(value));
        
        builder.Property(l => l.Horsepower).IsRequired()
            .HasConversion(
                hp => hp!.Value, 
                value => new Horsepower(value));
        
        builder.Property(l => l.Torque).IsRequired()
            .HasConversion(
                torque => torque!.Value, 
                value => new Torque(value));
        
        builder.Property(l => l.FuelType).IsRequired()
            .HasConversion(
                ft => ft.ToString(), 
                value => Enum.Parse<FuelType>(value));
        
        builder.Property(l => l.Aspiration).IsRequired()
            .HasConversion(
                aspiration => aspiration.ToString(),
                value => Enum.Parse<Aspiration>(value));
        
        builder.Property(l => l.EngineConfiguration).IsRequired()
            .HasConversion(
                ec => ec.ToString(),
                value => Enum.Parse<EngineConfiguration>(value));
        
        builder.Property(l => l.EngineLayout).IsRequired()
            .HasConversion(
                el => el.ToString(),
                value => Enum.Parse<EngineLayout>(value));
        #endregion //Engine
        
        #region Transmission
        builder.Property(l => l.TypeOfDrive).IsRequired()
            .HasConversion(
                tod => tod.ToString(),
                value => Enum.Parse<TypeOfDrive>(value));
        
        builder.Property(l => l.TransmissionType).IsRequired()
            .HasConversion(
                tt => tt.ToString(),
                value => Enum.Parse<TransmissionType>(value));
        #endregion //Transmission
        
        #region Body
        builder.Property(l => l.BodyType).IsRequired()
            .HasConversion(
                bt => bt.ToString(),
                value => Enum.Parse<BodyType>(value));
        
        builder.Property(l => l.Color).IsRequired()
            .HasConversion(
                color => color!.Value, 
                value => new Color(value))
            .HasMaxLength(ColorValidator.MaxLength);
        #endregion //Body
        
        #endregion //Car
        
        builder.Property(cl => cl.Price)
            .IsRequired()
            .HasConversion(
                price => price!.Value, 
                value => new Money(value));
        builder.Property(cl => cl.StartDate)
            .IsRequired()
            .HasConversion(
                date => DateTime.SpecifyKind(date, DateTimeKind.Utc),
                date => DateTime.SpecifyKind(date, DateTimeKind.Utc));
        
        builder.Property(l => l.Status)
            .IsRequired()
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<LotStatus>(value));
        
        builder.HasOne(l => l.Seller)
            .WithMany(s => s.ActiveListings)
            .HasForeignKey("SellerId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.Buyer)
            .WithMany()
            .HasForeignKey("BuyerId")
            .OnDelete(DeleteBehavior.Restrict);
    }
        
}