using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Orders;

public class WebsiteOrderConfig: IEntityTypeConfiguration<WebsiteOrder>
{
    public void Configure(EntityTypeBuilder<WebsiteOrder> builder)
    {
    }
}