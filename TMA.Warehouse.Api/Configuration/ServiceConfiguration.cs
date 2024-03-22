using TMA.Warehouse.Api.DataBase.Repository;
using TMA.Warehouse.Api.Seeding;
using TMA.Warehouse.Api.Services;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped(typeof(IDbHandle<>), typeof(DbHandle<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ITmaRequestService, TmaRequestService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<MainSeedManager>();
        }
    }
}
