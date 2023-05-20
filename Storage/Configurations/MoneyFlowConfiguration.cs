using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarketSimulationsRest.Storage.Models;

namespace StockMarketSimulationsRest.Storage.Configurations
{
    public class MoneyFlowConfiguration : IEntityTypeConfiguration<MoneyFlow>
    {
        public void Configure(EntityTypeBuilder<MoneyFlow> builder)
        {
            builder.HasKey(b => b.TransactionId);
            builder.Property(b => b.TransactionId).ValueGeneratedOnAdd();

            builder.Property(b => b.UserId).IsRequired().HasMaxLength(128);
            builder.Property(b => b.TransactionDate).IsRequired();
            builder.Property(b => b.TransactionValue).IsRequired();
            builder.Property(b => b.IsWithdraw).IsRequired();
            builder.HasIndex(b => b.UserId);
        }
    }
}
