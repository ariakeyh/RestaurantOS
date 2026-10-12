using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Polls;

public class PollFoodRatingConfig : IEntityTypeConfiguration<PollFoodRating>
{
    public void Configure(EntityTypeBuilder<PollFoodRating> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rating).HasMaxLength(512);

        builder.HasOne(x => x.MenuItem)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.MenuItemId);
        
        builder.HasOne(x => x.Response)
            .WithMany(x => x.FoodRatings)
            .HasForeignKey(x => x.ResponseId);
    }
}