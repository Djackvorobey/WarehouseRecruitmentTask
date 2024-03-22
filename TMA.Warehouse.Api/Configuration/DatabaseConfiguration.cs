using Microsoft.EntityFrameworkCore;
using TMA.Warehouse.Api.DataBase;

namespace TMA.Warehouse.Api.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"));
            });
        }
    }
}
