using TMA.Warehouse.Api.Models;

namespace TMA.Warehouse.Api.Dtos
{
    public class ItemAccountDto
    {
        public ItemModel ItemModel { get; set; }

        public AccountModel AccountModel { get; set; }
    }
}
