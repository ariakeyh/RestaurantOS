using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Settings;

public class ProvinceConfig : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(256);
        builder.HasMany(x=>x.Cities).WithOne(x=>x.Province).HasForeignKey(x=>x.ProvinceId);
    }
}