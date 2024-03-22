using System.Linq.Expressions;
using TMA.Warehouse.Api.DataBase;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly WarehouseDbContext _dbContext;

        public RoleService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Role> GetAll()
        {
            return _dbContext.Roles.AsQueryable();
        }

        public IQueryable<Role> GetAll(Expression<Func<Role, bool>> predicate)
        {
            return _dbContext.Roles.AsQueryable();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Role Insert(Role entity)
        {
            if (entity == null)
            {
                return null;
            }

            var role = _dbContext.Roles.Add(entity);

            _dbContext.SaveChanges();

            return role.Entity;
        }

        public void Update(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
