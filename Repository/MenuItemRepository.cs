using CalorieCalculator.Models;
using CalorieCalculatorCore.Data;
using Microsoft.EntityFrameworkCore;

namespace CalorieCalculatorCore.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //TODO extract include logic to method parameters
        public IEnumerable<MenuItem> GetAll()
        {
            return _context.MenuItems.AsEnumerable();
        }
        //TODO extract include logic to method parameters
        public MenuItem GetById(int menuItemID)
        {
            return _context.MenuItems
                            .Include(m => m.Product)
                            .SingleOrDefault(m => m.Id == menuItemID);
        }
        public void Insert(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
        }
        public void Update(MenuItem menuItem)
        {
            _context.Entry(menuItem).State = EntityState.Modified;
        }
        public void Delete(int menuItemID)
        {
            MenuItem menuItem = _context.MenuItems.Find(menuItemID);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }
        }
        public void Delete(MenuItem menuItem)
        {
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}