using Domain.Entities.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Menu;

public class CategoryConfig: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).HasMaxLength(64);
        builder.Property(x=>x.ImageName).HasMaxLength(2056);
        
        builder.HasOne(x=>x.Shop).WithMany(x=>x.Categories).HasForeignKey(x=>x.ShopId);
        builder.HasMany(x=> x.Items).WithOne(x=>x.Category).HasForeignKey(x=>x.CategoryId);
    }
}