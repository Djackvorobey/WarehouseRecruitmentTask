namespace TMA.Warehouse.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.Enums.RoleEnums RoleEnum { get; set; }
    }
}
