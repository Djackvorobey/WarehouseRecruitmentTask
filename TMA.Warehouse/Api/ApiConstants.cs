namespace TMA.Warehouse.Api
{
    public class ApiConstants
    {
        public const string RegisterUrl = "https://localhost:7036/api/Account/register";
        public const string AuthenticateUrl = "https://localhost:7036/api/Account/login";
        public const string AccountAllUrl = "https://localhost:7036/api/Account/accounts";
        public const string AccountRemoveUrl = "https://localhost:7036/api/Account/remove";
        public const string AccountUpdateUrl = "https://localhost:7036/api/Account/update";
        public const string ItemUrl = "https://localhost:7036/api/Item";
        public const string ItemAllUrl = "https://localhost:7036/api/Item/items";
        public const string TmaRequestUrl = "https://localhost:7036/api/TmaRequest";
        public const string TmaRequestConfirmUrl = "https://localhost:7036/api/TmaRequest/confirm";
        public const string TmaRequestRejectUrl = "https://localhost:7036/api/TmaRequest/reject";
    }
}
