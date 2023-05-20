using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarketSimulationsRest.Storage.Models;

namespace StockMarketSimulationsRest.Storage.Configurations
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.HasKey(b => b.TransactionId);
            builder.Property(b => b.TransactionId).ValueGeneratedOnAdd();

            builder.Property(b => b.UserId).IsRequired().HasMaxLength(128);
            builder.Property(b => b.TransactionDate).IsRequired();
            builder.Property(b => b.TransactionValue).IsRequired();

            builder.Property(b => b.StockPriceAtMoment).IsRequired();
            builder.Property(b => b.StockCount).IsRequired();
            builder.Property(b => b.IsBuy).IsRequired();
            builder.Property(b => b.StockId).IsRequired();
            builder.HasIndex(b => b.UserId);
        }
    }
}
