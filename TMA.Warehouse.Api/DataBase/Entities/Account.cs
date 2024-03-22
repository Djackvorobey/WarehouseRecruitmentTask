using System.ComponentModel.DataAnnotations.Schema;

namespace TMA.Warehouse.Api.DataBase.Entities
{
    [Table("Accounts")]
    public class Account : ModelBase
    {
        public string Name { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? RoleIdNavigation { get; set; }
    }
}
