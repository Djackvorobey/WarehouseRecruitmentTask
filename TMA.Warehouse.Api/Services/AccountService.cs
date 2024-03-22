using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TMA.Warehouse.Api.DataBase;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly WarehouseDbContext _dbContext;

        public AccountService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Account entity)
        {
            if (entity == null)
            {
                return;
            }

            _dbContext.Accounts.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<Account> GetAll()
        {
            return _dbContext.Accounts.AsQueryable();
        }

        public IQueryable<Account> GetAll(Expression<Func<Account, bool>> predicate)
        {
            return _dbContext.Accounts.Where(predicate);
        }

        public Account GetById(int id)
        {
            if (id == null)
            {
                return new Account();
            }

            return _dbContext.Accounts.Find(id);
        }

        public Account Insert(Account entity)
        {
            if (entity == null)
            {
                return null;
            }

            var account =  _dbContext.Accounts.Add(entity);
            _dbContext.SaveChanges();

            return account.Entity;
        }

        public void Update(Account entity)
        {
            if (entity == null)
            {
                return;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Account Authenticate(string username)
        {
            var user = _dbContext.Accounts.SingleOrDefault(x => x.Name == username);

            return user;
        }
    }
}
