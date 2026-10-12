using Domain.Entities.Sms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Sms;

public class SmsPriceSettingConfig : IEntityTypeConfiguration<SmsPriceSetting>
{
    public void Configure(EntityTypeBuilder<SmsPriceSetting> builder)
    {
        builder.HasKey(x => x.Id);
    }
}