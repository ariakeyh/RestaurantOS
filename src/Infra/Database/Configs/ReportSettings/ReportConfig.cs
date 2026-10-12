using Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.ReportSettings;

public class ReportConfig : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Name).HasMaxLength(64).IsRequired();
        builder.Property(x=>x.ApiUrl).HasMaxLength(256).IsRequired();
        builder.Property(x=>x.Description).HasMaxLength(1024);
        
        builder.HasOne(x=>x.ReportCategory).WithMany(x=>x.Reports).HasForeignKey(x=>x.ReportCategoryId);
        builder.HasMany(x=>x.ShopUserReports).WithOne(x=>x.Report).HasForeignKey(x=>x.ReportId);
    }
}