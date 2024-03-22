using static TMA.Warehouse.Api.DataBase.Enums.ItemGroups;
using static TMA.Warehouse.Api.DataBase.Enums.UnitOfMeasurements;

namespace TMA.Warehouse.Api.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ItemGroup ItemGroup { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Status { get; set; }

        public string StorageLocation { get; set; }

        public string ContactPerson { get; set; }

        public string Photo { get; set; }
    }
}
