using Domain.Entities.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class ShopCategoryAssignmentConfig : IEntityTypeConfiguration<ShopCategoryAssignment>
{
    public void Configure(EntityTypeBuilder<ShopCategoryAssignment> builder)
    {
        builder.HasKey(x => new { x.ShopId, x.CategoryId });

        builder.HasOne(x => x.Shop).WithMany(x => x.ShopCategories).HasForeignKey(x => x.ShopId);
        builder.HasOne(x => x.Category).WithMany(x => x.Shops).HasForeignKey(x => x.CategoryId);
    }
}