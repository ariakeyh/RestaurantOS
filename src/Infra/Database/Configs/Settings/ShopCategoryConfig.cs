using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Settings;

public class ShopCategoryConfig : IEntityTypeConfiguration<ShopCategory>
{
    public void Configure(EntityTypeBuilder<ShopCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(64);
    }
}