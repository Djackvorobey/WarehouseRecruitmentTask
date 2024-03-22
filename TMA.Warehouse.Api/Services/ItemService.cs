using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TMA.Warehouse.Api.DataBase;
using TMA.Warehouse.Api.DataBase.Entities;
using TMA.Warehouse.Api.Services.IServices;

namespace TMA.Warehouse.Api.Services
{
    public class ItemService : IItemService
    {
        private readonly WarehouseDbContext _context;

        public ItemService(WarehouseDbContext context)
        {
            _context = context;
        }

        public IQueryable<Item> GetAll()
        {
            return _context.Items;
        }

        public IQueryable<Item> GetAll(Expression<Func<Item, bool>> predicate)
        {
            return _context.Items.Where(predicate);
        }

        public Item GetById(int id)
        {
            if (id == null)
            {
                return new Item();
            }

            return _context.Items.Find(id);
        }

        public Item Insert(Item entity)
        {
            if (entity == null)
            {
                return null;
            }

            var item =  _context.Items.Add(entity);
            _context.SaveChanges();

            return item.Entity;
        }

        public void Update(Item entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Item entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Items.Remove(entity);
            _context.SaveChanges();
        }
    }
}
