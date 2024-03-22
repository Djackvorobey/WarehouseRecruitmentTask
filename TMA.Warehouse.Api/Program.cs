using Microsoft.EntityFrameworkCore;
using TMA.Warehouse.Api.Configuration;
using TMA.Warehouse.Api.DataBase;
using TMA.Warehouse.Api.Seeding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.ConfigureDatabase(configuration);

builder.Services.ConfigureApplicationServices(configuration);

builder.Services.AddAutoMapper(typeof(TMA.Warehouse.Api.Models.AutoMapper.AutoMapper));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
            dbContext.Database.Migrate();

            var seedManager = scope.ServiceProvider.GetRequiredService<MainSeedManager>();

            seedManager.SeedAllDb();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while migrating the database: " + ex.Message);
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
