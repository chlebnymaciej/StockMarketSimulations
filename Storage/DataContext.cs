using Microsoft.EntityFrameworkCore;
using StockMarketSimulationsRest.Storage.Configurations;
using StockMarketSimulationsRest.Storage.Models;

namespace StockMarketSimulationsRest.Storage
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("Simulations"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new MoneyFlowConfiguration());
            builder.ApplyConfiguration(new StockTransactionConfiguration());
        }
        public DbSet<MoneyFlow> UsersWallets { get; set; }
        public DbSet<StockTransaction> UsersTransactions { get; set; }
    }
}
