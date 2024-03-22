using System.ComponentModel.DataAnnotations;

namespace TMA.Warehouse.Api.DataBase.Entities
{
    [Serializable]
    public class ModelBase
    {
        [Key]
        public int Id { get; set; }
    }
}
