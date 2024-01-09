using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
