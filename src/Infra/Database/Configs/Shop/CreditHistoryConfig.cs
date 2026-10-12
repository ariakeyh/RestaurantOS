using Domain.Entities.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class CreditHistoryConfig : IEntityTypeConfiguration<CreditHistory>
{
    public void Configure(EntityTypeBuilder<CreditHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x=>x.Shop)
            .WithMany(x=>x.CreditHistories)
            .HasForeignKey(x=>x.ShopId);
    }   
}