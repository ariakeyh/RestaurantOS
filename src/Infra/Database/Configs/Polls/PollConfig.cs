using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Polls;

public class PollConfig : IEntityTypeConfiguration<Poll>
{
    public void Configure(EntityTypeBuilder<Poll> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Shop)
            .WithOne()
            .HasForeignKey<Poll>(x => x.ShopId);

        builder.HasMany(x => x.Questions)
            .WithOne(x => x.Poll)
            .HasForeignKey(x => x.PollId);
    }
}