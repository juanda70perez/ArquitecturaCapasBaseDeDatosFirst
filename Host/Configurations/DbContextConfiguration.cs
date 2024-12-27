
using Dal.Context;
using Microsoft.EntityFrameworkCore;

namespace WebHost.Configurations
{
    public static class DbContextConfiguration
    {
        public static void AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Connection"))
            );
        }
    }
}
