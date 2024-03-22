using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.DataBase.Repository;

namespace TMA.Warehouse.Api.Services.IServices
{
    public interface IAccountService : IDbHandle<Account>
    {
        public Account Authenticate(string username);
    }
}
