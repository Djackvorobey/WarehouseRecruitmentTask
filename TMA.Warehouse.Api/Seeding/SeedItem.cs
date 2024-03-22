using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.DataBase.Enums;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Seeding
{
    public class SeedItem
    {
        private readonly IItemService _itemService;
        public SeedItem(IItemService itemService)
        {
            _itemService = itemService;
        }
        public void SeedItems()
        {
            if (_itemService.GetAll().Any())
            {
                return;
            }

            List<Item> items = new();

            items.Add(new Item(){Name = "Nokia 3310", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Electronics, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 5000, Status = "New"});
            items.Add(new Item(){Name = "Samsung G20", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Electronics, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 10548, Status = "Used"});
            items.Add(new Item(){Name = "Lenovo Y2", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Electronics, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 5999, Status = "Used"});

            items.Add(new Item(){Name = "Metro 2033", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Books, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 243, Status = "New"});
            items.Add(new Item(){Name = "Пригоди Капітана Небрехи", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Books, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 500, Status = "New"});
            items.Add(new Item(){Name = "Metro 2035", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Books, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 243, Status = "New"});

            items.Add(new Item(){Name = "Baby Born", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Toys, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 765, Status = "New"});
            items.Add(new Item(){Name = "Запорожець", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Toys, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 9999, Status = "New"});
            items.Add(new Item(){Name = "Батіг", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Toys, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 276, Status = "New"});

            items.Add(new Item(){Name = "Ziemniak", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Food, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Kilogram, PriceWithoutVAT = 5, Status = "New"});
            items.Add(new Item(){Name = "Hamburger", Quantity = 100, ItemGroup = ItemGroups.ItemGroup.Food, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Unit, PriceWithoutVAT = 21, Status = "New"});
            items.Add(new Item(){Name = "Sugar", Quantity = 1000, ItemGroup = ItemGroups.ItemGroup.Food, UnitOfMeasurement = UnitOfMeasurements.UnitOfMeasurement.Kilogram, PriceWithoutVAT = 32, Status = "New"});

            foreach (var role in items)
            {
                _itemService.Insert(role);
            }
        }
    }
}
