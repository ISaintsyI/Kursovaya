
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHost(args).Build();

            //using var scope = host.Services.CreateScope();
            //var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //context.Database.Migrate();

            host.Run();
        }

        public static IHostBuilder CreateWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {  webBuilder.UseStartup<Startup>(); });
    }
}