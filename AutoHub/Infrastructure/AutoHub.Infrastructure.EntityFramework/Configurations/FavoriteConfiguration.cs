using AutoHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.Infrastructure.EntityFramework.Configurations;

public class FavoriteConfiguration: IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).IsRequired();

        builder.HasOne(f => f.Listing)
            .WithMany()
            .HasForeignKey("ListingId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
//AutoSpot.WebHost