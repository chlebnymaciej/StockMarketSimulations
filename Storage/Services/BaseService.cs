using Microsoft.EntityFrameworkCore;
using StockMarketSimulationsRest.Storage.Models;

namespace StockMarketSimulationsRest.Storage.Services
{
    public class BaseService<T>
        where T : BaseModel
    {
        protected readonly DataContext context;
        private readonly DbSet<T> dbSet;

        public BaseService(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            entity.TransactionDate = entity.TransactionDate = DateTime.Now;
            dbSet.Add(entity);
            context.SaveChanges();
        }


        public List<T> Read(string userId)
        {
            var data = dbSet.Where(o => o.UserId == userId).ToList<T>();
            return data;
        }
    }
}
