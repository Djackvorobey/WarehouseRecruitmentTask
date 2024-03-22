namespace TMA.Warehouse.Models
{
    public class TmaRequestModel
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public int ItemId { get; set; }

        public Enums.Enums.UnitOfMeasurement UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Comment { get; set; }

        public Enums.Enums.Status Status { get; set; }

        public int RequestRowId { get; set; }
    }
}
