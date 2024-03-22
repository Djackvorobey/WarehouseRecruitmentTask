using static TMA.Warehouse.Api.DataBase.Enums.Statuses;
using static TMA.Warehouse.Api.DataBase.Enums.UnitOfMeasurements;

namespace TMA.Warehouse.Api.Models
{
    public class TmaRequestModel
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public int ItemId { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Comment { get; set; }

        public Status Status { get; set; }

        public int RequestRowId { get; set; }
    }
}
