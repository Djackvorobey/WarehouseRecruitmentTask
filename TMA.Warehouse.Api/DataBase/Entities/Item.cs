using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TMA.Warehouse.Api.DataBase.Enums.ItemGroups;
using static TMA.Warehouse.Api.DataBase.Enums.UnitOfMeasurements;

namespace TMA.Warehouse.Api.DataBase.Entities
{
    [Table("Items")]
    public class Item : ModelBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public ItemGroup ItemGroup { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceWithoutVAT { get; set; }

        [Required]
        public string Status { get; set; }

        public string? StorageLocation { get; set; }

        public string? ContactPerson { get; set; }
        
        public string? Photo { get; set; }

        public virtual ICollection<TmaRequest> TmaRequests { get; set; } = new List<TmaRequest>();
    }
}
