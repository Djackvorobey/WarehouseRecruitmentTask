using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TMA.Warehouse.Api.DataBase;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Services
{
    public class TmaRequestService : ITmaRequestService
    {
        private readonly WarehouseDbContext _context;

        public TmaRequestService(WarehouseDbContext context)
        {
            _context = context;
        }

        public IQueryable<TmaRequest> GetAll()
        {
            return _context.TmaRequests;
        }

        public IQueryable<TmaRequest> GetAll(Expression<Func<TmaRequest, bool>> predicate)
        {
            return _context.TmaRequests.Where(predicate);
        }

        public TmaRequest GetById(int id)
        {
            if(id == null)
            {
                return new TmaRequest();
            }

            return _context.TmaRequests.Find(id);
        }

        public TmaRequest Insert(TmaRequest entity)
        {
            if (entity == null)
            {
                return null;
            }

            var tmaRequest = _context.TmaRequests.Add(entity);
            _context.SaveChanges();

            return tmaRequest.Entity;
        }

        public void Update(TmaRequest entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(TmaRequest entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.TmaRequests.Remove(entity);
            _context.SaveChanges();
        }
    }
}
