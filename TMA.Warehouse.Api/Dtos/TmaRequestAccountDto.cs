using TMA.Warehouse.Api.Models;

namespace TMA.Warehouse.Api.Dtos
{
    public class TmaRequestAccountDto
    {
        public TmaRequestModel TmaRequestModel { get; set; }

        public AccountModel AccountModel { get; set; }
    }
}
