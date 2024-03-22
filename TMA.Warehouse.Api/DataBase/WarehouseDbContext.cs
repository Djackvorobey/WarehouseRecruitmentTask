using Microsoft.EntityFrameworkCore;
using TMA.Warehouse.Api.DataBase.Entities;

namespace TMA.Warehouse.Api.DataBase
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<TmaRequest> TmaRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
