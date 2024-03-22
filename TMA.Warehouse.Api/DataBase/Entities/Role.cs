using System.ComponentModel.DataAnnotations.Schema;
using static TMA.Warehouse.Api.DataBase.Enums.Roles;

namespace TMA.Warehouse.Api.DataBase.Entities
{
    [Table("Roles")]
    public class Role : ModelBase
    {
        [Column(TypeName = "VARCHAR(50)")]
        public RoleEnum RoleName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
