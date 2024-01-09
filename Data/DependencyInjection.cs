using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IRepository<Car>, CarRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
        }
    }
}
