using Domain.Entities.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class ShopUserConfig : IEntityTypeConfiguration<ShopUser>
{
    public void Configure(EntityTypeBuilder<ShopUser> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.UseTptMappingStrategy();
        builder.HasOne(x=>x.User).WithMany(x=>x.Shops).HasForeignKey(x=>x.UserId);
        builder.HasOne(x=>x.Shop).WithMany(x=>x.Users).HasForeignKey(x=>x.ShopId);
    }
}