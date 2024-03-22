using static TMA.Warehouse.Api.DataBase.Enums.UnitOfMeasurements;
using System.ComponentModel.DataAnnotations;
using static TMA.Warehouse.Api.DataBase.Enums.Statuses;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMA.Warehouse.Api.DataBase.Entities
{
    [Table("TmaRequests")]
    public class TmaRequest : ModelBase
    {
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceWithoutVAT { get; set; }

        public string Comment { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public Status Status { get; set; } 

        public int RequestRowId { get; set; } 

        [ForeignKey("ItemId")]
        public virtual Item? ItemIdNavigation { get; set; }
    }
}
