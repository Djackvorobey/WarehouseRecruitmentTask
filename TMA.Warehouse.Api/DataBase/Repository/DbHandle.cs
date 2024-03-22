using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TMA.Warehouse.Api.DataBase.Repository
{
    public class DbHandle<T> : IDbHandle<T> where T : class
    {
        private readonly WarehouseDbContext _context;
        private readonly DbSet<T> _dbSet;

        public DbHandle(WarehouseDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Insert(T entity)
        {
            var model = _dbSet.Add(entity);
            Save();
            return model.Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            Save();
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}
