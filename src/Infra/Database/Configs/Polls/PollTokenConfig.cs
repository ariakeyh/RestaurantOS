using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Polls;

public class PollTokenConfig : IEntityTypeConfiguration<PollToken>
{
    public void Configure(EntityTypeBuilder<PollToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token).IsRequired().HasMaxLength(64);
        builder.HasIndex(x=>x.Token).IsUnique();
        
        builder.HasOne(x=>x.Order).WithMany().HasForeignKey(x=>x.OrderId);
    }
}