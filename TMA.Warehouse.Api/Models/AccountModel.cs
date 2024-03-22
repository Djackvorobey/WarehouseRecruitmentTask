using TMA.Warehouse.Api.DataBase.Enums;

namespace TMA.Warehouse.Api.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Roles.RoleEnum RoleEnum { get; set; }
    }
}
