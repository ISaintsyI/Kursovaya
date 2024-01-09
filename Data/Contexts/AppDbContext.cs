using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Car> Cars { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
