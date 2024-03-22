namespace TMA.Warehouse.Models.Dtos
{
    public class ManageAccountRequestDto
    {
        public AccountModel AdminAccount { get; set; }

        public AccountModel UserAccount { get; set; }
    }
}
