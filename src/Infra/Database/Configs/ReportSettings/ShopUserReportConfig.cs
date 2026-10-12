using Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.ReportSettings;

public class ShopUserReportConfig : IEntityTypeConfiguration<ShopUserReport>
{
    public void Configure(EntityTypeBuilder<ShopUserReport> builder)
    {
        builder.HasKey(x => new { x.ShopUserId, x.ReportId });
        
        builder.HasOne(x => x.ShopUser).WithMany(x=>x.ShopUserReports).HasForeignKey(x => x.ShopUserId);
        builder.HasOne(x=>x.Report).WithMany(x=>x.ShopUserReports).HasForeignKey(x => x.ReportId);
    }
}