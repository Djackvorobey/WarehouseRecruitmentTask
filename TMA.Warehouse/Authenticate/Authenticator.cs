using TMA.Warehouse.Models;

namespace TMA.Warehouse.Authenticate
{
    public class Authenticator : IAuthenticator
    {
        public User AuthenticatedUser { get; set; }
    }
}
