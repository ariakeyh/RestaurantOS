using Domain.Entities.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class ShopCashbackSettingConfig : IEntityTypeConfiguration<ShopCashbackSetting>
{
    public void Configure(EntityTypeBuilder<ShopCashbackSetting> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x=>x.Shop)
            .WithMany(x=>x.CashbackSettings)
            .HasForeignKey(x=>x.ShopId);
    }
}