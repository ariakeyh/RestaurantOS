using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Auth;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.UseTptMappingStrategy();
        builder.Property(x=>x.Name).HasMaxLength(64);
        builder.Property(x=>x.PhoneNumber).HasMaxLength(11);
        builder.Property(x=>x.AdminPasswordHashed).HasMaxLength(2056);
        
        builder.HasMany(x=> x.Shops).WithOne(x=>x.User).HasForeignKey(x=>x.UserId);
    }
}