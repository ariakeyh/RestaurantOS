using Domain.Entities.Polls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Polls;

public class PollResponseConfig : IEntityTypeConfiguration<PollResponse>
{
    public void Configure(EntityTypeBuilder<PollResponse> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Note).HasMaxLength(512);
        
        builder.HasOne(x=>x.Order)
            .WithMany()
            .HasForeignKey(x=>x.OrderId);
    }
}