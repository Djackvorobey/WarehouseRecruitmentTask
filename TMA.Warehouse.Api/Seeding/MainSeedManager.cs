using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Seeding
{
    public class MainSeedManager
    {
        private readonly IRoleService _roleService;
        private readonly IItemService _itemService;
        public MainSeedManager(IRoleService roleService, IItemService itemService)
        {
            _roleService = roleService;
            _itemService = itemService;
        }
        public void SeedAllDb()
        {
            SeedRole seedRole = new SeedRole(_roleService);

            seedRole.SeedRoles();

            SeedItem seedItem = new SeedItem(_itemService);

            seedItem.SeedItems();
        }
    }
}
