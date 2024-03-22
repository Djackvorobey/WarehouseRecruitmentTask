namespace TMA.Warehouse.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.Enums.ItemGroup ItemGroup { get; set; }

        public Enums.Enums.UnitOfMeasurement UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Status { get; set; }

        public string StorageLocation { get; set; }

        public string ContactPerson { get; set; }

        public string Photo { get; set; }
    }
}
