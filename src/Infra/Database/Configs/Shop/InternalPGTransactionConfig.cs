using Domain.Entities.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.Configs.Shop;

public class InternalPGTransactionConfig : IEntityTypeConfiguration<InternalPGTransaction>
{
    public void Configure(EntityTypeBuilder<InternalPGTransaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.PGProvider, x.Authority });
        builder.Property(x => x.Authority).IsRequired().HasMaxLength(64);

        builder.HasOne(x => x.CreditHistory).WithOne(x => x.InternalPgTransaction)
            .HasForeignKey<InternalPGTransaction>(x => x.CreditHistoryId);
        builder.HasOne(x => x.Shop).WithMany(x => x.InternalPGTransactions).HasForeignKey(x => x.ShopId);
    }
}