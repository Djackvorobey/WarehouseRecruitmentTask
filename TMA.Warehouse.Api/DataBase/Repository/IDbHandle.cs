using System.Linq.Expressions;

namespace TMA.Warehouse.Api.DataBase.Repository
{
    public interface IDbHandle<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
