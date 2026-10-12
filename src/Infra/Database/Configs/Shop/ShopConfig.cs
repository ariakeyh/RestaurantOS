using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class ShopConfig: IEntityTypeConfiguration<Domain.Entities.Shops.Shop>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Shops.Shop> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).HasMaxLength(64);
        builder.Property(x=>x.Description).HasMaxLength(2048);
        builder.Property(x=>x.Address).HasMaxLength(2048);
        builder.Property(x=>x.ContactInfo).HasMaxLength(2048);
        
        builder.HasMany(x=> x.Users).WithOne(x=>x.Shop).HasForeignKey(x=>x.ShopId);
    }
}