namespace TMA.Warehouse.Models.Enums
{
    public class Enums
    {
        public enum RoleEnums
        {
            Employee = 1,

            Coordinator = 2,

            Administrator = 3
        }

        public enum ItemGroup
        {
            None,
            Electronics,
            Clothing,
            Books,
            Food,
            Toys
        }

        public enum UnitOfMeasurement
        {
            None,
            Kilogram,
            Gram,
            Liter,
            Milliliter,
            Unit
        }

        public enum Status
        {
            New,
            Approved,
            Rejected
        }
    }
}

