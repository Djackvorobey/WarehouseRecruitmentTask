using TMA.Warehouse.Models;

namespace TMA.Warehouse.Authenticate
{
    public interface IAuthenticator
    {
        User AuthenticatedUser { get; set; }
    }
}
