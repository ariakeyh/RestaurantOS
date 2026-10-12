using Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.ReportSettings;

public class ReportCategoryConfig : IEntityTypeConfiguration<ReportCategory>
{
    public void Configure(EntityTypeBuilder<ReportCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
        
        builder.HasMany(x=>x.Reports).WithOne(x=>x.ReportCategory).HasForeignKey(x=>x.ReportCategoryId);
    }
}