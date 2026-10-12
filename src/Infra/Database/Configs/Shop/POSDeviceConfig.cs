using Domain.Entities.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class POSDeviceConfig : IEntityTypeConfiguration<POSDevice>
{
    public void Configure(EntityTypeBuilder<POSDevice> builder)
    {
        builder.HasIndex(x=>x.IMEI).IsUnique();
        builder.HasIndex(x=>x.SerialNumber).IsUnique();
        
        builder.Property(x => x.SerialNumber).HasMaxLength(32);
        builder.Property(x => x.IMEI).HasMaxLength(16);
        builder.HasMany(x=>x.Orders).WithOne(x=>x.POSDevice).HasForeignKey(x=>x.POSDeviceId);
    }
}