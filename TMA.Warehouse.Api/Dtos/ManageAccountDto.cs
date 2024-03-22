using TMA.Warehouse.Api.Models;

namespace TMA.Warehouse.Api.Dtos
{
    public class ManageAccountDto
    {
        public AccountModel AdminAccount { get; set; }

        public AccountModel UserAccount { get; set; }

    }
}
